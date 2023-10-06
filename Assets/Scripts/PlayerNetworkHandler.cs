using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

/** <summary>Component on the individual player objects (mouse pointers)</summary> */
public class PlayerNetworkHandler : NetworkBehaviour
{

    private ChatWindow chatWindow;
    private MousePositioning mousePosition;
    public GameObject placeablePrefab;


    public override void OnNetworkSpawn()
    {
        chatWindow = GameObject.Find("ChatWindow").GetComponent<ChatWindow>();
        mousePosition = GetComponent<MousePositioning>();
        chatWindow.playerNetworkHandler = this;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SpawnObjectServerRpc(mousePosition.GetWorldPosition());
        }
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
    private void SpawnObjectServerRpc(Vector3 spawnPosition)
    {
        Debug.Log("Starting to work...");
        GameObject placed = Instantiate(placeablePrefab);
        NetworkObject nwo = placed.GetComponent<NetworkObject>();
        nwo.Spawn();
        placed.transform.position = spawnPosition;
        //placed.transform.rotation = activePreview.transform.rotation;
    }
}
