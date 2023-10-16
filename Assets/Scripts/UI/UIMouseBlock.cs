using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// On UI components to help detect when the cursor is hovering over a UI element.
/// </summary>
public class UIMouseBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIManager uiManager;
    private bool hovering = false;

    public void Awake()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        uiManager.SetHoveredElements(1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        uiManager.SetHoveredElements(-1);
    }

    public void OnDestroy()
    {
        if (hovering == true)
        {
            uiManager.SetHoveredElements(-1);
        }
    }
}
