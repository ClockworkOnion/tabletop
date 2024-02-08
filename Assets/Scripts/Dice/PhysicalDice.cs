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
    private bool isRollValid = false;
    private Vector3 rollDirection;
    public int sideCount;
    public Material castValidMaterial;
    public Material castInvalidMaterial;
    private bool hovered = false;
    [SerializeField] private bool dragging = false;
    private CameraControl camera;
    private PlayerNetworkHandler player;
    public GameObject LineRenderPrefab;
    private LineRenderer currentLine;
    

    Rigidbody rigidbody;

    void Start()
    {
        //GetComponent<DoubleClickListener>().doubleClicked.AddListener(Roll);
        camera = Camera.main.GetComponent<CameraControl>();
        sides = GetComponentsInChildren<DiceSide>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Code for clicking and dragging right mouse button. For more controlled throwing of the dice.
        if (hovered && Input.GetMouseButtonDown(1))
        {
            dragging = true;
            player = GetComponent<DoubleClickListener>().clickingPlayer;
        }

        if (dragging && Input.GetMouseButton(1))
        {
            camera.cameraLocked = true;

            DebugText.GetInstance().DisplayText("Rolldirection: " + rollDirection + "\nMagnitude: " + (player.transform.position - transform.position).magnitude);
            if (!currentLine)
            {
                LineRenderer line = Instantiate(LineRenderPrefab).GetComponent<LineRenderer>();
                currentLine = line;
            }

            Vector3[] positions = new Vector3[5];
            Vector3 dieToMouse = player.transform.position - transform.position;
            float magni = dieToMouse.magnitude;
            isRollValid = magni > 3;
            currentLine.material = isRollValid ? castValidMaterial : castInvalidMaterial;
            rollDirection = isRollValid ? dieToMouse : new Vector3(0, 0, 0);
            currentLine.positionCount = 5;

            positions[0] = transform.position;
            positions[4] = player.transform.position;

            // Create and set the positions to make an arc with the line renderer
            dieToMouse = player.transform.position - transform.position;
            magni = dieToMouse.magnitude * .25f;
            dieToMouse.Scale(new Vector3(.25f, .25f, .25f));
            dieToMouse.y += magni;
            positions[1] = transform.position + dieToMouse;

            dieToMouse = player.transform.position - transform.position;
            magni = dieToMouse.magnitude * .35f;
            dieToMouse.Scale(new Vector3(.5f, .5f, .5f));
            dieToMouse.y += magni;
            positions[2] = transform.position + dieToMouse;

            dieToMouse = player.transform.position - transform.position;
            magni = dieToMouse.magnitude * .25f;
            dieToMouse.Scale(new Vector3(.75f, .75f, .75f));
            dieToMouse.y += magni;
            positions[3] = transform.position + dieToMouse;
            
            currentLine.SetPositions(positions);

        }
        else if (dragging && !Input.GetMouseButton(1))
        {
            dragging = false;
            camera.cameraLocked = false;
            Destroy(currentLine.gameObject);
            currentLine = null;

            if (isRollValid)
                Roll(player, rollDirection);
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
        player.HandleChatMsgServerRpc(sideCount + " rolled a " + result);
    }

    public void Roll(PlayerNetworkHandler roller, Vector3 rollDirect)
    {
        rollDieServerRpc(rollDirect);
    }

    [ServerRpc(RequireOwnership = false)]
    public void rollDieServerRpc(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float forceFactor = 50f; // So the dice actually move
        //rb.AddTorque(direction);
        rb.AddTorque(new Vector3(direction.z*5, 0, direction.x*-5));
        direction.Scale(new Vector3(forceFactor, forceFactor, forceFactor));
        rb.AddForce(direction + new Vector3(0, direction.magnitude * 1.5f, 0));
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
