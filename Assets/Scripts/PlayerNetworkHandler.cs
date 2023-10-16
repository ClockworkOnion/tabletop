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
            //SpawnObjectServerRpc(mousePosition.GetWorldPosition()); // WORKS
            /*
            The PlayerNetworkHandler exists multiple times when the Host and Client are connected.
	        Since non-network components such as UI buttons can't distinguish between clients and
            basically can't determine which player pressed it, they can't "push" their information
            to a PlayerNetworkHandler or MousePositioning.
            Instead, PlayerNetworkHandler must poll local UI elements for which button is active.
            */

            placeablePrefab = PlaceableSelectPanel.selectedObject;
            if (placeablePrefab != null) SpawnObjectServerRpc(mousePosition.GetWorldPosition());
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

    /*
    Note that adressing class fields in here, they will always have the value
    of the server! Never that of the client. This is why some things must be
    passed along as parameters.

    Perhaps make a (static) list of creatable objects, with references to their prefabs
    and pass the integer along that points to a position in the list. (Or a map...)
    */
    [ServerRpc(RequireOwnership = false)]
    private void SpawnObjectServerRpc(Vector3 spawnPosition)
    {
        // GameObject placed = Instantiate(placeablePrefab); // this is ALWAYS the placeablePrefab on the server


    }
}
