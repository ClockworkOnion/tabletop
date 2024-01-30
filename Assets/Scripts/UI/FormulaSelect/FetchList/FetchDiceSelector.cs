using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class FetchDiceSelector : MonoBehaviour, FetchListSelector
{
    TextMeshProUGUI label;
    string rollToListenFor;
    int sideToListenFor;
    int amountToListenFor;
    List<int> results = new();

    private Image inputBG;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public float SelectedValue()
    {
        int finalResult = 0;
        results.ForEach((result) => finalResult += result);
        return finalResult;
    }

    public bool IsValid() { return amountToListenFor == 0; }

    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        inputBG = GetComponentInChildren<Image>();
        UpdateLabel();
    }

    void UpdateLabel()
    {
        string newText = amountToListenFor == 0 ? "" : "Roll " + amountToListenFor.ToString() + " more d" + sideToListenFor + " to fill! ";
        if (results.Count > 0)
            newText += "Rolled: ";
        results.ForEach((result) => newText += result.ToString() + ", ");
        newText.TrimEnd();
        newText.TrimEnd(',');
        label.text = newText;
    }


    public void ToFetch(string fetchString)
    {
        rollToListenFor = fetchString;
        string[] parts = rollToListenFor.Split('d');
        amountToListenFor = Int32.Parse(parts[0]);
        sideToListenFor = Int32.Parse(parts[1]);
    }

    public string GetLabel()
    {
        return "!" + rollToListenFor;
    }

    public void ListenToRoll(int sides, int rollResult)
    {
        if (sides != sideToListenFor || amountToListenFor == 0)
            return;

        results.Add(rollResult);
        amountToListenFor--;

        UpdateLabel();

        if (amountToListenFor == 0)
            inputBG.color = new Color(0.52f, 0.91f, 0.52f);
    }
}
