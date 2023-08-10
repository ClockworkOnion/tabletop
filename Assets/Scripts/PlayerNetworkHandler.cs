using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetworkHandler : NetworkBehaviour
{

    private ChatWindow chatWindow;


    public override void OnNetworkSpawn()
    {
        chatWindow = GameObject.Find("ChatWindow").GetComponent<ChatWindow>();
        chatWindow.playerNetworkHandler = this;
    }


    #region chat window
    // Receive a string from the local chat window and pass it along
    // to all clients.

    [ClientRpc]
    public void DistributeMsgClientRpc(string msg)
    {
        chatWindow.SubmitChatMessage(msg);
    }

    [ServerRpc(RequireOwnership = false)]
    public void HandleChatMsgServerRpc(string msg)
    {
        DistributeMsgClientRpc(msg);
    }
    #endregion
}
