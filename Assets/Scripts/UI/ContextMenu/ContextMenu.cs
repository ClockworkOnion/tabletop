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

    public List<GameObject> menuItemsRefs = new(); // Remember instantiated prefabs for later deletion

    private GameObject formulaBtnRef; // Reference to the instantiated prefab
    [Header("Prefabs")]
    public GameObject attachFormulaButtonPrefab;
    public GameObject attributeTextPrefab;
    public GameObject formulaTriggerBtnPrefab;

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
        ClearMenu();

        // Basics: Delete & Lock position
        if (current)
        {
            menuGraphics.SetHeader(current.pickupName);

            deleteButton.onClick.RemoveAllListeners();
            deleteButton.onClick.AddListener(OnDeleteClick);

            positionLockToggle.isOn = current.positionLocked;
            positionLockToggle.onValueChanged.AddListener(OnTogglePositionLock);
        }

        // Attach formula if none is attached
        if (!target.GetComponent<Actor>())
        {
            formulaBtnRef = Instantiate(attachFormulaButtonPrefab);
            formulaBtnRef.transform.SetParent(menuGraphics.transform);
            formulaBtnRef.GetComponent<AttachFormulaButton>().SetFormulaTarget(current.gameObject, this);
            menuItemsRefs.Add(formulaBtnRef);
        }

        if (target.GetComponent<Actor>() is Actor actor) // = Formula already attached
        {

            // Display attributes from Dictionary
            foreach (KeyValuePair<string, float> attribute in actor.attributes)
            {
                GameObject attribDisplay = Instantiate(attributeTextPrefab);
                attribDisplay.GetComponent<TextMeshProUGUI>().SetText(attribute.Key + " : " + attribute.Value);
                attribDisplay.transform.SetParent(menuGraphics.transform);
                menuItemsRefs.Add(attribDisplay);
            }

            // Display formulas from Dictionary
            foreach (KeyValuePair<string, List<string>> attribute in actor.formulas)
            {
                GameObject formulaTrigger = Instantiate(formulaTriggerBtnPrefab);
                formulaTrigger.transform.SetParent(menuGraphics.transform);
                formulaTrigger.GetComponent<FormulaTrigger>().SetData(attribute, actor);
                menuItemsRefs.Add(formulaTrigger);
            }
        }

    }

    private void ClearMenu()
    {
        foreach (GameObject item in menuItemsRefs)
        {
            Destroy(item);
        }
        menuItemsRefs = new();
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
