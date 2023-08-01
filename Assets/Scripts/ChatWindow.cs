using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatWindow : MonoBehaviour
{
    TMP_InputField inputField;
    TextMeshProUGUI chatLogDisplay;
    List<string> chatLogText = new List<string>();
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
            chatLogText.Add(inputField.text);
            chatLogQueue.Enqueue(inputField.text);
            inputField.text = "";

            if (chatLogQueue.Count >= 30) {
                chatLogQueue.Dequeue();
	    }

            chatLogDisplay.SetText(string.Join("\n", chatLogQueue));
        }
    }

    public void onSubmitChatMessage(string inputText)
    {
        Debug.Log("Pressed enter:" + inputText);
    }
}
