using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugText : MonoBehaviour
{
    private TextMeshProUGUI displayText;
    private static DebugText instance;


    private void Awake()
    {
        displayText = GetComponent<TextMeshProUGUI>();

        if (instance == null) instance = this;
    }

    public static DebugText GetInstance() {
        return instance;
    }

    public void DisplayText(string newText) {
        displayText.text = newText;
    }
}
