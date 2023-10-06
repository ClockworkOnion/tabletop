using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

/** <summary>Reads the mouse position on the screen and translates it into commands
for things like moving objects. Component on individual player objects (mouse pointer).</summary> */
public class MousePositioning : NetworkBehaviour
{

    [SerializeField] private Vector3 screenPosition;
    [SerializeField] private Vector3 worldPosition;

    private Pickupable heldObject;

    private DebugText debugText;

    public override void OnNetworkSpawn()
    {
        // Set up debug text window
        debugText = GameObject.Find("DebugText").GetComponent<DebugText>();
        if (debugText == null) { Debug.Log("No debugtext found!"); }
        debugText.DisplayText("It works!");
    }

    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;

            // Handle click and release of the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                Pickupable target = hitData.transform.GetComponent<Pickupable>();
                if (target)
                {
                    heldObject = target;
                    heldObject.gameObject.layer = 1 << 1; // Layer "ignore raycast", also prevents other players from trying to grab it.
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                DropObject();
            }

            // Move the held object to the correct position, as long as it's being held
            if (heldObject)
            {
                BoxCollider boxCollider = heldObject.GetComponent<BoxCollider>(); // is this OK to do on a non-owned network object?
                //BoxCollider boxCollider = heldObject.GetBoxColliderServerRpc(); // won't work like this
                Vector3 colliderHeight = boxCollider.bounds.size;

                Vector3 floatPosition = worldPosition + Vector3.up * colliderHeight.y / 2;
                //heldObject.transform.position = floatPosition; // Works only for the server
                heldObject.moveObjectServerRpc(floatPosition);


                debugText.DisplayText("Worldposition: " + floatPosition.ToString() + "\nColliderheight: " + colliderHeight.y);

                // Rotate object
                //heldObject.transform.Rotate(new Vector3(0, Input.mouseScrollDelta.y * Time.deltaTime * rotationSpeed, 0)); // Server only
                float rotation = Input.mouseScrollDelta.y * Time.deltaTime * StaticRefs.rotationSpeed;
                heldObject.RotateObjectServerRpc(rotation);
            }


            if (Input.GetMouseButton(0))
            {
                //Debug.Log(Input.mouseScrollDelta);
            }


        }
        else
        {
            DropObject();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TestServerRpc();
        }


        transform.position = worldPosition;
        // Consider using plane raycast

    }

    private void DropObject()
    {
        if (heldObject)
        {
            heldObject.gameObject.layer = 0; // Back to original layer
            heldObject = null;
        }
    }

    [ServerRpc]
    private void TestServerRpc()
    {
        Debug.Log("ServerRPC" + OwnerClientId);
    }

    public Vector3 GetWorldPosition() {
        return worldPosition;
    }
}
