using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatWindow : MonoBehaviour
{
    TMP_InputField inputField;
    TextMeshProUGUI chatLogDisplay;
    Queue<string> chatLogQueue = new Queue<string>();

    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        chatLogDisplay = GameObject.Find("ChatText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputField.text != "")
        {
            SubmitChatMessage(inputField.text);
            inputField.text = "";
        }
    }

    public void SubmitChatMessage(string inputText)
    {
        chatLogQueue.Enqueue(inputText);
        if (chatLogQueue.Count >= 30) chatLogQueue.Dequeue();
        chatLogDisplay.SetText(string.Join("\n", chatLogQueue));
    }
}

