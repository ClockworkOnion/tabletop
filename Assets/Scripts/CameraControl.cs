using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float cameraMoveSpeed = 40f;
    Vector3 mouseLastPosition = new Vector3(0, 0, 0);
    Vector3 initialCamPosition;

    // rotation
    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;

    void Awake()
    {
        initialCamPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            if (Input.GetMouseButton(0))
            {
                transform.Translate((mouseLastPosition - Input.mousePosition) * cameraMoveSpeed * Time.deltaTime);
            }

            if (Input.GetMouseButton(1))
            {
                currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
                currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
                currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
                currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
                Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            }

            transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y * cameraMoveSpeed));

        }

        mouseLastPosition = Input.mousePosition;
    }
}