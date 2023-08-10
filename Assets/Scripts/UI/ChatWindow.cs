using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ChatWindow : MonoBehaviour
{
    TMP_InputField inputField;
    TextMeshProUGUI chatLogDisplay;
    Queue<string> chatLogQueue = new Queue<string>();

    public PlayerNetworkHandler playerNetworkHandler;

    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        chatLogDisplay = GameObject.Find("ChatText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && inputField.text != "")
        {
            //SubmitChatMessage(inputField.text);
            NetworkChatMessage(inputField.text);
            inputField.text = "";
        }
    }

    public void SubmitChatMessage(string inputText)
    {
        chatLogQueue.Enqueue(inputText);
        if (chatLogQueue.Count >= 30) chatLogQueue.Dequeue();
        chatLogDisplay.SetText(string.Join("\n", chatLogQueue));
    }

    public void NetworkChatMessage(string inputText) {
        if (playerNetworkHandler == null)
        {
            SubmitChatMessage("Start or connect to server to use chat!");
            return;
        }

        playerNetworkHandler.HandleChatMsgServerRpc(inputText);

    }
}

/*
Notes:
The ChatWindow itself is not a network object. It's not synchronized by the network.
Although that might be possible, it would require the chat window to be a prefab,
which would need to be part of the network prefabs list.
The server would then (unless disabled?) synchronize the transform of the chat window,
which led to unforeseen behavior.

As it is, the chat window will use the local player object to pass its messages to.
The player object (PlayerNetworkHandler) will take the string, pass it to the server,
which distributes it to all players, including back to the player who sent the message.
*/

