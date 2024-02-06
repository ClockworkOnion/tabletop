using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class PhysicalDice : NetworkBehaviour
{
    [SerializeField] private int displayedSide = 0;
    DiceSide[] sides;

    private bool isRolling = false;
    public int sideCount;
    private bool hovered = false;
    [SerializeField] private bool dragging = false;
    private CameraControl camera;
    private PlayerNetworkHandler player;
    public GameObject LineRenderPrefab;

    Vector3 lastPosition = new();
    Quaternion lastRotation = new();
    Rigidbody rigidbody;

    void Start()
    {
        GetComponent<DoubleClickListener>().doubleClicked.AddListener(Roll);
        camera = Camera.main.GetComponent<CameraControl>();
        sides = GetComponentsInChildren<DiceSide>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Code for clicking and dragging right mouse button. For more controlled throwing of the dice.
        // LineRenderer component is in use by the Outline (?) and is deactivated when not hovering
        // Maybe make another object for the line renderer?
        if (hovered && Input.GetMouseButtonDown(1))
        {
            Debug.Log("MouseDragOn");
            dragging = true;
            player = GetComponent<DoubleClickListener>().clickingPlayer;
        }

        if (dragging && Input.GetMouseButton(1))
        {
            camera.cameraLocked = true;

            if (!TryGetComponent(out LineRenderer line)) // Cancel if there's no line renderer
            {
                dragging = false;
                camera.cameraLocked = false;
                return;
            }

            DebugText.GetInstance().DisplayText("Dragging line to " + player.transform.position.ToString());
            line.SetPosition(1, player.transform.position);
        }
        else if (dragging && !Input.GetMouseButton(1))
        {
            dragging = false;
            camera.cameraLocked = false;

        }

        if (!isRolling)
            return;

        if (rigidbody.velocity.magnitude < 0.001f)
        {
            DiceSide topside = sides[0];
            foreach (DiceSide side in sides)
                if (side.transform.position.y > topside.transform.position.y)
                    topside = side;
            displayedSide = topside.value;
            isRolling = false;
            EmitResult(displayedSide);
        }
    }

    private void EmitResult(int result)
    {
        List<FetchDiceSelector> diceListeners = GameObject.Find("FetchlistPanel").GetComponentsInChildren<FetchDiceSelector>().Cast<FetchDiceSelector>().ToList();
        diceListeners.ForEach((die) => die.ListenToRoll(sideCount, rollResult: result));
    }

    private void LateUpdate()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    public void Roll(PlayerNetworkHandler roller)
    {
        rollDieServerRpc(new Vector3(0, 0, 0));
    }

    [ServerRpc(RequireOwnership = false)]
    public void rollDieServerRpc(Vector3 newPos)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(200, 200, 200));
        rb.AddTorque(new Vector3(100, 100, 100));
        LeanTween.delayedCall(.5f, () => { isRolling = true; });
    }

    private void OnMouseEnter()
    {
        hovered = true;
    }
    private void OnMouseExit()
    {
        hovered = false;
    }
}
