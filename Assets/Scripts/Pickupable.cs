using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Pickupable : NetworkBehaviour
{
    public string pickupName = "";
    public string description = "";
    public bool positionLocked = false; // TODO: Allow host to lock position

    [ServerRpc(RequireOwnership = false)]
    public void moveObjectServerRpc(Vector3 newPos)
    {
        transform.position = newPos;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RotateObjectServerRpc(float rotation)
    {
        transform.Rotate(new Vector3(0, rotation, 0));
    }

    // Won't work because the boxcollider is a reference object
    //[ServerRpc(RequireOwnership = false)]
    //public BoxCollider GetBoxColliderServerRpc() {
    //    return gameObject.GetComponent<BoxCollider>();
    //}
}

