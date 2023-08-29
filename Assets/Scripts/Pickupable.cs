using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Pickupable : NetworkBehaviour
{
    Vector3 initialScale;
    Vector3 newScale;

    public string name = "";
    public string description = "";
    private LTDescr delay;
    private const float TOOLTIP_DELAY_TIME = 0.5f;
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

    public void OnMouseEnter() {
        delay = LeanTween.delayedCall(TOOLTIP_DELAY_TIME, () =>
        {
            TooltipSystem.Show(name, description);
        });
    }

    public void OnMouseExit() { 
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }

}

