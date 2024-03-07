using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PanButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Orientation orientation = Orientation.xy;
    private Vector2 fixPosition = new Vector2(100, 100);
    private bool positionFixed = false;
    private CameraControl camControl;

    private void Start()
    {
        camControl = Camera.main.GetComponent<CameraControl>();
    }

    public void Update()
    {
        if (positionFixed)
        {
            Mouse.current.WarpCursorPosition(fixPosition);
            Vector3 displacement = new (0, 0, 0);

            switch (orientation)
            {
                case Orientation.xy:
                    displacement = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
                    break;
                case Orientation.zx:
                    displacement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
                    break;
                case Orientation.yz:
                    displacement = new Vector3(0, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                    break;
                case Orientation.pivot:
                    break;
                default:
                    break;
            }

            camControl.Move(displacement * 25f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointerdown");
        fixPosition = new Vector2(Mouse.current.position.x.value, Mouse.current.position.y.value);

        positionFixed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointerUp");
        positionFixed = false;
    }

    public enum Orientation { xy, zx, yz, pivot }
}
