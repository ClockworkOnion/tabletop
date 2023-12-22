using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FormulaEvaluator : MonoBehaviour
{
    public GameObject UIPanelChild;
    public Button evalButton;

    private Actor performingActor;
    private List<string> formulaToEvaluate;
    private PlayerNetworkHandler requestingPlayer;

    [Header("Fetch Item prefabs")]
    public GameObject selectActorPartnerRow;
    public GameObject enterValueRow;
    public GameObject diceListenerRow;

    public void Evaluate(List<string> _formula, string formulaName, Actor _actor, PlayerNetworkHandler requester)
    {
        formulaToEvaluate = _formula;
        performingActor = _actor;
        requestingPlayer = requester;

        List<string> fetchList = BuildFetchList();
        // Create panel for each found fetchItem
        fetchList.ForEach(x => CreateFetchItem(x));

        if (fetchList.Count == 0)
        {
            Debug.Log("Fetchlist was empty. Evaluating.");
            string formula = ConcatList(_formula);
            ExpressionEvaluator.Evaluate(formula, out float result);
            requester.HandleChatMsgServerRpc("Result: " + result.ToString());
        }

    }

    public List<string> BuildFetchList()
    {
        List<string> results = new();
        foreach (string chunk in formulaToEvaluate)
        {
            if (chunk[0] == '@' || chunk[0] == '$' || chunk[0] == '!')
                results.Add(chunk);
        }
        return results;
    }

    public void CreateFetchItem(string item)
    {
        switch (item[0])
        {
            case '@':
                // create SelectActorPartner entry
                break;
            case '$':
                // create EnterValue entry
                break;
            case '!':
                // create DiceListener entry
                break;
            default:
                Debug.LogError("Unkown prefix for Fetchlist in " + item);
                break;
        }
    }

    // Hidden for now, perhaps build on this later, when FetchList is done.
    public List<string> RealEvaluate()
    {
        Debug.Log("Evaluating: " + ConcatList(formulaToEvaluate));

        List<string> fetchList = new();
        List<string> cleanedFormula = new();

        // First get values from attributes, inputfields or dice rolls.
        // The fetchlist must be empty before we can continue!
        // Call it "chunk" to not overlap with playfield tokens.
        foreach (string chunk in formulaToEvaluate)
        {
            // Is it a float already? Then fine.
            if (float.TryParse(chunk, out float tokenValue))
            {
                cleanedFormula.Add(chunk);
                continue;
            }

            // Not a float, so maybe an operator, or something that needs to be fetched?
            char first = chunk[0];
            switch (first)
            {
                case '@':
                case '$':
                case '!':
                    // Add to Fetchlist: Fetch attribute from another token -> float
                    fetchList.Add(chunk);
                    continue;
                case '+':
                case '-':
                case '*':
                case '%':
                case '=':
                    // We know it's an assignment now.
                    cleanedFormula.Add(chunk);
                    continue;
            }

            // At this point, it should be an existing attribute, so check them
            foreach (KeyValuePair<string, float> attribute in performingActor.attributes)
            {
                if (chunk == attribute.Key)
                    cleanedFormula.Add(attribute.Value.ToString());
                continue;
            }

            Debug.LogError("Couldn't evaluate chunk with value '" + chunk + "'. Did you misspell an attribute?");
        }

        return fetchList;
    }

    public string ConcatList(List<string> strings)
    {
        string result = "";
        strings.ForEach(x => result += x + " ");
        return result;
    }
}

public class FetchValueEvent : UnityEvent<float> { }