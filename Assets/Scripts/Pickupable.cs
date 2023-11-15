using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Pickupable : NetworkBehaviour
{
    public string pickupName = "";
    public string description = "";
    public bool positionLocked = false; // TODO: Allow host to lock position (context menu)

    [ServerRpc(RequireOwnership = false)]
    public void moveObjectServerRpc(Vector3 newPos)
    {
        if (!positionLocked)
			transform.position = newPos;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RotateObjectServerRpc(float rotation)
    {
        if (!positionLocked)
			transform.Rotate(new Vector3(0, rotation, 0));
    }

    // Won't work because the boxcollider is a reference object
    //[ServerRpc(RequireOwnership = false)]
    //public BoxCollider GetBoxColliderServerRpc() {
    //    return gameObject.GetComponent<BoxCollider>();
    //}

    [ServerRpc(RequireOwnership = false)]
    public void DeleteObjectServerRpc()
    {
        Destroy(gameObject);
    }
}

