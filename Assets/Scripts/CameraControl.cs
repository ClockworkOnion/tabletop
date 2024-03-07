using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private readonly float cameraMoveSpeed = 20f;
    private Vector3 mouseLastPosition = new(0, 0, 0);
    private Vector3 initialCamPosition;
    private Quaternion initialCamRotation;
    private readonly float lookSensitivityBase = 10f;
    private readonly float maxYAngle = 80f;
    private Vector2 currentRotation;
    private OptionsPanel optionsMenu;
    public Transform lookAtTarget;
    public bool cameraLocked = false;

    void Awake()
    {
        initialCamPosition = transform.position;
        optionsMenu = GameObject.Find("OptionsPanel").GetComponent<OptionsPanel>();
        initialCamPosition = transform.position;
        initialCamRotation = transform.rotation;
        StoreRotation();
    }

    void Update()
    {
        if (!UIManager.GetInstance().IsHoveringUI() && !cameraLocked)
            HandleMovementInput();
    }

    public void ResetPosition()
    {
        transform.SetPositionAndRotation(initialCamPosition, initialCamRotation);
        StoreRotation();
    }

    private void HandleMovementInput()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                PanCamera();
            else
                PivotCamera();
        }

        if (Input.GetMouseButton(0))
            LookAround();

        ZoomView();

        mouseLastPosition = Input.mousePosition;
    }

    private void ZoomView()
    {
        float zoomFactor = optionsMenu.GetScrollSensitivity();
        transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y * zoomFactor * cameraMoveSpeed));
    }

    private void LookAround()
    {
        float rotationFactor = optionsMenu.GetLookSensitiviy();
        currentRotation.x += Input.GetAxis("Mouse X") * lookSensitivityBase * rotationFactor;
        currentRotation.y -= Input.GetAxis("Mouse Y") * lookSensitivityBase * rotationFactor;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        // Why do I need to pass y and x in reverse here? Axis "sideways"?
        Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        SetPivot();
    }

    private void PanCamera()
    {
        float panFactor = optionsMenu.GetPanSensitiviy();
        transform.Translate((mouseLastPosition - Input.mousePosition) * cameraMoveSpeed * panFactor * Time.deltaTime);
    }

    private void PivotCamera()
    {
        RaycastHit ray = SetPivot();
        float panFactor = optionsMenu.GetPanSensitiviy();
        transform.Translate((mouseLastPosition - Input.mousePosition) * cameraMoveSpeed * panFactor * Time.deltaTime);
        transform.LookAt(ray.point);
        StoreRotation();
    }

    private RaycastHit SetPivot()
    {
        RaycastHit ray;
        Physics.Raycast(transform.position, transform.forward, out ray);
        lookAtTarget.position = ray.point;
        return ray;
    }

    private void StoreRotation()
    {
        // x and y need to be assigned in reverse here, to make up for the reverse passing into the Quaternion.Euler function
        currentRotation.x = transform.localEulerAngles.y;
        currentRotation.y = transform.localEulerAngles.x;
        DebugText.GetInstance().DisplayText("x: " + currentRotation.x.ToString() + " y: " + currentRotation.y.ToString());
    }

    public void MoveTo(Vector3 newPosition) {
        transform.position = newPosition;
    }

    public void Move(Vector3 displacement)
    {
        transform.position += optionsMenu.GetPanSensitiviy() * Time.deltaTime * displacement;
    }

    public void MovePivot(Vector3 displacement) { 
        RaycastHit ray = SetPivot();
        float panFactor = optionsMenu.GetPanSensitiviy();
        transform.Translate(displacement * cameraMoveSpeed * panFactor * Time.deltaTime);
        transform.LookAt(ray.point);
        StoreRotation();
    }

}
