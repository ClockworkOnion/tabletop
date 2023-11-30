using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ContextMenu : MonoBehaviour
{
    public ContextMenuTarget target { get; private set; }
    public Button deleteButton;
    public Toggle positionLockToggle;
    public bool hoveringTarget = false;
    private RectTransform rectTransform;
    private MenuGraphics menuGraphics;

    public ContextMenuTarget hoveredToken;

    [Header("Prefabs")]
    public GameObject attachFormulaButtonPrefab;
    private GameObject formulaBtnRef;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        menuGraphics = GameObject.Find("MenuGraphics").GetComponent<MenuGraphics>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && hoveringTarget)
        {
            target = hoveredToken;
            SetPosition();
            SetContents();
        }

        if (!menuGraphics.isMouserPointerInside && Input.GetMouseButtonDown(0))
        {
            HideMenu();
        }
    }

    public void SetContents()
    {
        Pickupable current = target.GetComponent<Pickupable>();
        if (current)
        {
            menuGraphics.SetHeader(current.pickupName);

            deleteButton.onClick.RemoveAllListeners();
            deleteButton.onClick.AddListener(OnDeleteClick);

            positionLockToggle.isOn = current.positionLocked;
            positionLockToggle.onValueChanged.AddListener(OnTogglePositionLock);
        }

        if (!target.GetComponent<Actor>() && !formulaBtnRef)
        {
            formulaBtnRef = Instantiate(attachFormulaButtonPrefab);
            formulaBtnRef.transform.SetParent(menuGraphics.transform);
            formulaBtnRef.GetComponent<AttachFormulaButton>().SetFormulaTarget(current.gameObject, this);
        }
    }

    private void OnDeleteClick()
    {
        target.GetComponent<Pickupable>().DeleteObjectServerRpc();
    }

    private void OnTogglePositionLock(bool onOrOff)
    {
        target.GetComponent<Pickupable>().SetPositionLockServerRpc(onOrOff);
    }

    private void SetPosition()
    {
        menuGraphics.gameObject.SetActive(true);
        transform.position = Input.mousePosition;
        // magic values 1.1, -0.1 etc are the offsets that work well to position the tooltip
        float xPivot = ((rectTransform.sizeDelta.x + rectTransform.position.x) > Screen.width) ? 1.1f : -0.1f;
        float yPivot = ((-rectTransform.sizeDelta.y + rectTransform.position.y) < 0) ? -0.5f : 1.1f;
        rectTransform.pivot = new Vector2(xPivot, yPivot);
    }

    /// <summary>
    /// Allows tokens with a context menu to set themselves as the target, or reset the target to null when the mouse pointer leaves them.
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(ContextMenuTarget _target)
    {
        target = _target;
    }

    public void HideMenu()
    {
        menuGraphics.gameObject.SetActive(false);
    }
}
