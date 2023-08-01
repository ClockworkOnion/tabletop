using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositioning : MonoBehaviour
{

    [SerializeField] private Vector3 screenPosition;
    [SerializeField] private Vector3 worldPosition;

    private Pickupable heldObject;
    private float rotationSpeed = 400f; // TODO: Make this adjustable via menu

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
                //Debug.Log("Clicked at: " + hitData.transform.name);
                Pickupable target = hitData.transform.GetComponent<Pickupable>();
                if (target)
                {
                    heldObject = target;
                    heldObject.gameObject.layer = 1 << 1; // Layer "ignore raycast"
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                DropObject();
            }

            // Move the held object to the correct position, as long as it's being held
            if (heldObject)
            {
                BoxCollider boxCollider = heldObject.GetComponent<BoxCollider>();
                Vector3 colliderHeight = boxCollider.bounds.size;

                Vector3 floatPosition = worldPosition + Vector3.up * colliderHeight.y / 2;
                heldObject.transform.position = floatPosition;

                // Rotate object
                heldObject.transform.Rotate(new Vector3(0, Input.mouseScrollDelta.y * Time.deltaTime * rotationSpeed, 0));
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



        transform.position = worldPosition;
        // Consider using plane raycast

    }

    private void DropObject()
    {
        if (heldObject)
        {
            heldObject.gameObject.layer = 0;
            heldObject = null;
        }
    }
}
