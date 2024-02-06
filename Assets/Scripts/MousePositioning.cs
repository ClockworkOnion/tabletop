using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;

/** <summary>Reads the mouse position on the screen and translates it into commands
for things like moving objects. Component on individual player objects (mouse pointer).</summary> */
public class MousePositioning : NetworkBehaviour
{

    [SerializeField] private Vector3 screenPosition;
    [SerializeField] private Vector3 worldPosition;

    private Pickupable heldObject;
    private Vector3 heldObjectOffset; // to prevent object from "jumping" to the mouse
    private GameObject hoveredObject; // Stores the hovered object

    private DebugText debugText;
    private CameraControl camControl;
    private PlayerNetworkHandler playerNetworkHandler;

    public override void OnNetworkSpawn()
    {
        // Set up debug text window
        debugText = GameObject.Find("DebugText").GetComponent<DebugText>();
        if (debugText == null) { Debug.Log("No debugtext found!"); }
        debugText.DisplayText("Debug text here");

        // Connect to local camera controller
        camControl = Camera.main.GetComponent<CameraControl>();

        playerNetworkHandler = GetComponent<PlayerNetworkHandler>();
    }

    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            hoveredObject = hitData.transform.gameObject;
            worldPosition = hitData.point;

            // Pass PlayerNetworkHandler to dice (or other doubleclicked things)
            if (hitData.transform.GetComponent<DoubleClickListener>() is DoubleClickListener diceRoller)
                diceRoller.SetNetworkHandler(playerNetworkHandler);

            // Handle click and release of the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                Pickupable target = hitData.transform.GetComponent<Pickupable>();
                if (target)
                {
                    camControl.cameraLocked = true;
                    heldObject = target;
                    heldObject.gameObject.layer = 1 << 1; // Layer "ignore raycast", also prevents other players from trying to grab it.

                    // Update world position, now that heldObject ignores raycast.
                    Ray newRay = Camera.main.ScreenPointToRay(screenPosition);
                    if (Physics.Raycast(newRay, out RaycastHit newHitData))
                        worldPosition = newHitData.point;

                    heldObjectOffset = target.transform.position - worldPosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                DropObject();
            }

            // Move the held object to the correct position, as long as it's being held
            if (heldObject)
            {
                //BoxCollider boxCollider = heldObject.GetComponent<BoxCollider>(); // is this OK to do on a non-owned network object?
                //Vector3 colliderHeight = boxCollider.bounds.size;
                Vector3 floatPosition = worldPosition; // ' + Vector3.up * colliderHeight.y / 2; // or place the pivot at the bottom and don't touch colliderheight otherwise
                heldObject.moveObjectServerRpc(floatPosition + heldObjectOffset);
                float rotation = Input.mouseScrollDelta.y * Time.deltaTime * StaticRefs.rotationSpeed;
                heldObject.RotateObjectServerRpc(rotation);
                //debugText.DisplayText("Worldposition: " + floatPosition.ToString() + "\nColliderheight: " + colliderHeight.y);

                // Change offset by cursor
                if (Input.GetKey(KeyCode.DownArrow))
                    heldObjectOffset = new Vector3(heldObjectOffset.x, heldObjectOffset.y - .01f, heldObjectOffset.z);
                if (Input.GetKey(KeyCode.UpArrow))
                    heldObjectOffset = new Vector3(heldObjectOffset.x, heldObjectOffset.y + .01f, heldObjectOffset.z);
            }
        }
        else
        {
            DropObject();
            hoveredObject = null;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TestServerRpc();
        }

        transform.position = worldPosition;
        // Consider using plane raycast? (But maybe not necessary after all...)
    }

    public GameObject GetHovered() {
        return hoveredObject;
    }

    public bool IsHoldingObject()
    {
        return heldObject ? true : false;
    }

    private void DropObject()
    {
        camControl.cameraLocked = false;
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

    public Vector3 GetWorldPosition()
    {
        return worldPosition;
    }


}
