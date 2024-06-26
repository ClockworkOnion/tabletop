using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is attached to a token gameobject that will allow the context menu to open on right-click.
/// </summary>
public class ContextMenuTarget : MonoBehaviour
{
    private ContextMenu contextMenu;
    private Outline outline;

    private void Start()
    {
        contextMenu = GameObject.Find("ContextMenu").GetComponent<ContextMenu>();
        if (!contextMenu) Debug.LogError("Failed to find ContextMenu Object/Component! Context Menu will not work. Did you rename something?");
    }

    private void OnMouseEnter()
    {
        contextMenu.hoveringTarget = true;
        contextMenu.hoveredToken = this;
    }

    private void OnMouseExit()
    {
        contextMenu.hoveringTarget = false;
        contextMenu.hoveredToken = null;
    }

} 