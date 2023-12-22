using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class EnterValueSelector : MonoBehaviour
{
    TMP_InputField valueInput;
    FetchValueEvent enterValueEvent = new();
    Image inputBG;

    private void Start()
    {
        valueInput = GetComponentInChildren<TMP_InputField>();
        valueInput.onValueChanged.AddListener(delegate { OnValueInput(); });
        inputBG = GetComponentInChildren<Image>();
    }

    private void OnValueInput()
    {
        if (float.TryParse(valueInput.text, out float enteredFloat))
        {
            enterValueEvent.Invoke(enteredFloat);
            inputBG.color = new Color(0.52f, 0.91f, 0.52f);
        }
        else
        {
            inputBG.color = new Color(0.35f, 0.38f, 0.45f);
        }
    }
}
