using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class EnterValueSelector : MonoBehaviour, FetchListSelector
{

    private float selectedValue = -256f; // default to unlikely value to catch bugs lol
    public TextMeshProUGUI label;
    string valueName;
    TMP_InputField valueInput;
    Image inputBG;

    private void Start()
    {
        valueInput = GetComponentInChildren<TMP_InputField>();
        valueInput.onValueChanged.AddListener(delegate { OnValueInput(); });
        inputBG = GetComponentInChildren<Image>();
        label.text = "Enter value for " + valueName;

    }

    private void OnValueInput()
    {
        if (float.TryParse(valueInput.text, out float enteredFloat))
        {
            inputBG.color = new Color(0.52f, 0.91f, 0.52f);
            selectedValue = enteredFloat;
        }
        else
        {
            inputBG.color = new Color(0.35f, 0.38f, 0.45f);
        }
    }

    public float SelectedValue()
    {
        if (selectedValue == -256f)
            Debug.LogError("Warning, selected value still at -256, something didn't work right?");
        Debug.Log("EnterValueSelector returns " + selectedValue);
        return selectedValue;
    }

    public bool IsValid()
    {
        return float.TryParse(valueInput.text, out _);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ToFetch(string fetchString)
    {
        valueName = fetchString;
    }

    public string GetLabel()
    {
        return "$" + valueName;
    }
}
