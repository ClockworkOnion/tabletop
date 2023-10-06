using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// On the UI button for selecting placeable items. It handles graphical highlighting
/// as well as instantiation of a semi-transparent preview and the actual object.
/// </summary>
public class PlaceableSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject placeablePrefab;
    public GameObject previewPrefab;
    public PlacementPreview activePreview;
    private Image btnImage;
    private bool isActive = false;
    private PlaceableSelectPanel placeableSelectPanel;

    private Color activeColor = StaticRefs.activeColor;
    private Color defaultColor = StaticRefs.defaultColor;
    private Color hoverColor = StaticRefs.hoverColor;
    private Color hoverActiveColor = StaticRefs.hoverActiveColor;

    public void Awake()
    {
        btnImage = GetComponent<Image>();
        placeableSelectPanel = GameObject.Find("DecoPanel").GetComponent<PlaceableSelectPanel>();
        placeableSelectPanel.Subscribe(this);
    }

    public void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(1))
        {
            Activate(false);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Before SpawnRpc");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnImage.color = isActive ? hoverActiveColor : hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnImage.color = isActive ? activeColor : defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isActive)
        {
            // Only deactivate self if active
            Activate(false);
            return;
        }

        // Disable all panels, but activate this one
        placeableSelectPanel.DisableChildren();
        Activate(true);
    }

    /// <summary>
    /// Sets the active (or "clicked") status of the UI button
    /// </summary>
    /// <param name="status">Clicked, or not clicked?</param>
    public void Activate(bool status)
    {
        isActive = status;
        btnImage.color = (status) ? hoverActiveColor : defaultColor;

        // Spawn the preview prefab
        if (status && activePreview == null)
        {
            activePreview = Instantiate(previewPrefab).GetComponent<PlacementPreview>();
        }
        else if (!status && activePreview != null)
        {
            Destroy(activePreview.gameObject);
        }

    }
}
