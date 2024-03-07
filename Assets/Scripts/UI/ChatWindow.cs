using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
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

    public void NetworkChatMessage(string inputText)
    {
        if (playerNetworkHandler == null)
        {
            SubmitChatMessage("Start or connect to server to use chat!");
            return;
        }

        string parsedMessage = ParseChatMessage(inputText);
        playerNetworkHandler.HandleChatMsgServerRpc(parsedMessage);
    }

    /// <summary>
    /// Parse and transform the string if necessary
    /// </summary>
    /// <param name="inputText">The player's input text. Commands begin with forward slash: /</param>
    /// <returns></returns>
    public string ParseChatMessage(string inputText)
    {
        if (inputText[0] != '/') return inputText;
        string returnText = inputText;
        string[] tokens = inputText.Split(' ');
        if (tokens[0] == "/roll" || tokens[0] == "/r") return DiceRoller(tokens[1]);
        return "Currently supported commands are /roll or /r for dice rolls";
    }

    public string DiceRoller(string input)
    {
        string dicePattern = @"^\d+d\d+$";
        if (!Regex.IsMatch(input, dicePattern)) return "Follow /roll with a dice pattern such as xdy, i.e. 1d6, 2d20 etc.";
        string[] numbers = input.Split('d');
        int.TryParse(numbers[0], out int count);
        int.TryParse(numbers[1], out int die);
        int result = 0;
        for (int i = 0; i < count; i++)
        {
            result += Random.Range(1, die + 1);
        }
        return "/Rolled " + input + ", result: " + result.ToString();
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

