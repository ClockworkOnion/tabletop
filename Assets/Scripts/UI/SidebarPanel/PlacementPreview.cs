using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is used on the half transparent preview models for placing
/// objects into the world. It updates the position to move along the
/// mouse cursor and stores and returns its rotation.
/// </summary>
public class PlacementPreview : MonoBehaviour
{
    Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main; // or just use this directly!?
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (StaticRefs.groundPlane.Raycast(ray, out float hitDistance))
        {
            Vector3 hitPoint = ray.GetPoint(hitDistance);
            transform.position = hitPoint;
        }

        // Rotate by mousewheel
        float rotation = Input.mouseScrollDelta.y * Time.deltaTime * StaticRefs.rotationSpeed;
        transform.Rotate(new Vector3(0, rotation, 0));
    }




}
