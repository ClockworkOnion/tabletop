using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private Vector2 lastMousePosition;
    private Vector2 mouseDelta;

    void Update()
    {
        mouseDelta = (Vector2)Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;   
    }

    public Vector2 GetMovement() {
        return mouseDelta;
    }
}
