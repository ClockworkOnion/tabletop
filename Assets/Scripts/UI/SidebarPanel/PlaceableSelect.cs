using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// On the UI button for selecting placeable items. It handles graphical highlighting
/// as well as instantiation of a semi-transparent preview and the actual object.
/// </summary>
public class PlaceableSelect : PanelButton
{
    public GameObject placeablePrefab;
    public GameObject previewPrefab;
    public PlacementPreview activePreview;
    public string prefabName; // Must be filled in inspector!

    public override void Start()
    {
        parentPanel = GameObject.Find("DecoPanel").GetComponent<PlaceableSelectPanel>();
        parentPanel.Subscribe(this);
    }

    public void Update()
    {
        if (!isActive) return;
        if (Input.GetMouseButtonDown(1)) 
	        Activate(false);
    }

    /// <summary>
    /// Sets the active (or "clicked") status of the UI button
    /// In this case, it means the user can preview and place
    /// the selected item in the world.
    /// </summary>
    /// <param name="status">Clicked, or not clicked?</param>
    public override void Activate(bool status)
    {
        isActive = status;
        btnImage.color = (status) ? hoverActiveColor : defaultColor;

        // Spawn the preview prefab
        if (status && activePreview == null)
        {
            PlaceableSelectPanel.selectedObject = placeablePrefab;
            activePreview = Instantiate(previewPrefab).GetComponent<PlacementPreview>();
            activePreview.toPlace = placeablePrefab;
            DebugText.GetInstance().DisplayText(placeablePrefab.name);
        }
        else if (!status && activePreview != null)
        {
            PlaceableSelectPanel.selectedObject = null;
            DebugText.GetInstance().DisplayText(placeablePrefab.name);
            Destroy(activePreview.gameObject);
        }

    }
}
