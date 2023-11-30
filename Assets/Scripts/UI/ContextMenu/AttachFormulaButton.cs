using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// On a button that shows up in the context menu when there's no formula attached to the actor
/// </summary>
public class AttachFormulaButton : MonoBehaviour, IPointerClickHandler
{
    private GameObject formulaTarget;
    private ContextMenu contextMenu;

    public GameObject selectPanelPrefab; // Assign in inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        // Create the selector panel and pass values
        GameObject selectPanel = Instantiate(selectPanelPrefab);
        selectPanel.GetComponent<FormulaFileSelector>().targetActor = formulaTarget;
        selectPanel.transform.SetParent(GameObject.Find("Canvas").transform);
        RectTransform panelTransform = selectPanel.GetComponent<RectTransform>();
        panelTransform.position = contextMenu.GetComponent<RectTransform>().position;

        // Clean up
        contextMenu.HideMenu();
        UIManager.GetInstance().SetHoveredElements(-1); // Because pointer never leaves the menu
        Destroy(gameObject);
    }

    public void SetFormulaTarget(GameObject target, ContextMenu _contextMenu) {
        formulaTarget = target;
        contextMenu = _contextMenu;
    }

}
