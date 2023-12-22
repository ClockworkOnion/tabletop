using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A component on each button in the right-click context menu, which is used in the UI to trigger the formula.
/// It will pass the formula to the evaluator.
/// </summary>
public class FormulaTrigger : MonoBehaviour
{
    string formulaName = "";
    List<string> formula;
    private Actor actor;

    private FormulaEvaluator evaluator;

    void Start()
    {
        TooltipTrigger tooltip = GetComponent<TooltipTrigger>();
        tooltip.header = formulaName;
        tooltip.content = ConcatList(formula);

        GetComponentInChildren<TextMeshProUGUI>().SetText(formulaName);
        evaluator = GameObject.Find("FormulaEvaluator").GetComponent<FormulaEvaluator>();
    }

    public void EvaluateFormula()
    {
        // TODO Find a  way to get reference to the clicking player (PlayerNetworkHandler) here and pass it along. Needed for the Chat window
        // IDEA: Right click listener, like the DoubleClickListener, catches the PlayerNetworkHandler as the context menu is created.
        // The PlayerNetworkHandler can then be passed along to the respective buttons.
        PlayerNetworkHandler anyPlayer = GameObject.Find("PlayerSphere(Clone)").GetComponent<PlayerNetworkHandler>();
        evaluator.Evaluate(formula, formulaName, actor, anyPlayer);
    }

    // Replacement for a constructor
    public void SetData(KeyValuePair<string, List<string>> data, Actor _actor)
    {
        formulaName = data.Key;
        formula = data.Value;
        actor = _actor;
    }

    public string ConcatList(List<string> strings)
    {
        string result = "";
        strings.ForEach(x => result += x + " ");
        return result;
    }

    public bool IsFloat(string toCheck)
    {
        float result;
        return float.TryParse(toCheck, out result);
    }
}
