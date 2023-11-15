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

    public GameObject chestPrefab;

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

            bool hoveringUI = UIManager.GetInstance().IsHoveringUI();
            placeablePrefab = PlaceableSelectPanel.selectedObject;
            if (!hoveringUI && placeablePrefab != null) SpawnObjectServerRpc(mousePosition.GetWorldPosition(), new Quaternion(), placeablePrefab.name); // TODO: Pass along rotation
        }
    }

    #region chat window fold
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
    Remember that adressing class fields in here, they will always have the value
    of the server, never that of the client. This is why some things must be
    passed along as parameters (like the prefabName).
    */
    [ServerRpc(RequireOwnership = false)]
    private void SpawnObjectServerRpc(Vector3 spawnPosition, Quaternion spawnRotation, string prefabName)
    {
        GameObject placed = Instantiate(UIManager.GetInstance().GetPrefabByName(prefabName)); // always the placeablePrefab on the server
        placed.GetComponent<NetworkObject>().Spawn();
        placed.transform.position = spawnPosition;
        placed.transform.rotation = spawnRotation;
    }
}
