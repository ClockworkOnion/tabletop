using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Main component on the "FetchList" window. Here we receive the formula from the context menu, evaluate it (directly, or
/// create the menu entries to allow the player to fill in the missing values) and then write back the results into the
/// console or the respective actor's attributes.
/// </summary>
public class FormulaEvaluator : MonoBehaviour
{
    private Actor performingActor; // For getting / writing attributes
    private List<string> formulaToEvaluate; // This gets updated throughout the evaluation process
    private PlayerNetworkHandler requestingPlayer; // For access to the chat window and the performing players name

    public GameObject fetchListPanel; // Assigned in inspector, the parent for the "Fetch Item prefabs"
    private List<FetchListSelector> selectors = new(); // List of selectors from which to poll the required values

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

        if (fetchList.Count == 0)
        {
            Debug.Log("Fetchlist was empty. Evaluating.");
            string formula = ConcatList(_formula);
            ExpressionEvaluator.Evaluate(formula, out float result);
            requester.HandleChatMsgServerRpc("Result: " + result.ToString());
            return;
        }

        /*
		    If we're here, the fetch List must have entries.
			So show the FetchList window (this component) and wait for the player to
            fill all entries and press evaluate (or cancel the evaluation)
        */

        ClearFetchList();
        // Create panel for each found fetchItem
        fetchList.ForEach(x => CreateFetchItem(x));
    }

    public void OnEvaluateClick()
    {
        /*
			First, check if every FetchList entry has a menu item with a valid value. If yes, we can evaluate.
			Go through the formula. Replace every non-static entry in the formula with it's value from the
			menu items. Afterwards, the formula can be evaluated.
        */

        selectors.ForEach((x) =>
        {
            if (x.IsValid() == false)
            {
                // TODO: Display some kind of message visible to the user
                Debug.Log("Not all selectors have values yet. Fill them and click Evaluate again.");
                return;
            }
        });


        Dictionary<string, float> replacementValues = new();
        selectors.ForEach((selector) => { replacementValues.Add(selector.GetLabel(), selector.SelectedValue()); });

        // //print dictionary
        //foreach (KeyValuePair<string, float> entry in replacementValues)
        //{
        //    Debug.Log("Printing dictionary...");
        //    Debug.Log(entry.Key + " : " + entry.Value);
        //    Debug.Log("Dictionary finished.");
        //}

        // Replace values -> Can't use foreach loop because it's read-only
        for (int i = 0; i < formulaToEvaluate.Count - 1; i++)
            if (replacementValues.ContainsKey(formulaToEvaluate[i]))
                formulaToEvaluate[i] = replacementValues[formulaToEvaluate[i]].ToString();

        Debug.Log("All values were filled. Evaluating...");
        string formula = ConcatList(formulaToEvaluate);
        Debug.Log("Formula: " + formula);
        ExpressionEvaluator.Evaluate(formula, out float result);
        requestingPlayer.HandleChatMsgServerRpc("Result: " + result.ToString());

    }

    private void ClearFetchList()
    {
        selectors.ForEach((x) => { Destroy(x.GetGameObject()); });
        selectors = new();
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
                PartnerTokenSelector newRow = CreateEntry(selectActorPartnerRow) as PartnerTokenSelector;
                newRow.mousePosition = requestingPlayer.GetComponent<MousePositioning>();
                break;
            case '$':
                CreateEntry(enterValueRow);
                break;
            case '!':
                CreateEntry(diceListenerRow);
                break;
            default:
                Debug.LogError("Unkown prefix for Fetchlist in " + item);
                break;
        }

        FetchListSelector CreateEntry(GameObject toCreate)
        {
            GameObject newEntry = Instantiate(toCreate);
            newEntry.transform.SetParent(fetchListPanel.transform);
            FetchListSelector newSelector = newEntry.GetComponent<FetchListSelector>();
            newSelector.ToFetch(item[1..^0]);
            selectors.Add(newSelector);
            return newSelector;
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