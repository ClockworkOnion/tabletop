using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

/** <summary>Component on the individual player objects (mouse pointers)</summary> */
public class PlayerNetworkHandler : NetworkBehaviour
{

    private ChatWindow chatWindow;
    public GameObject placeablePrefab;


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

    [ServerRpc(RequireOwnership = false)]
    private void SpawnObjectServerRpc()
    {
        Debug.Log("Starting to work...");
        GameObject placed = Instantiate(placeablePrefab);
        Debug.Log("Problem with the GameObject? " + placed == null);
        NetworkObject nwo = placed.GetComponent<NetworkObject>();
        Debug.Log("Problem with the Network object? " + nwo == null);
        nwo.Spawn();
        //placed.transform.position = activePreview.transform.position;
        //placed.transform.rotation = activePreview.transform.rotation;
    }
}
