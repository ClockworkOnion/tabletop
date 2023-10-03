using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    public ContextMenuTarget target { get; private set; }
    private RectTransform rectTransform;
    private MenuGraphics menuGraphics;

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
        if (Input.GetMouseButtonDown(1) && target != null)
        {
            SetPosition();
            SetContents();
        }

        //if (menuGraphics.isMouserPointerInside)

    }



    public void SetContents()
    {
        // If there's a pickupable component (and there should be),
        // its name becomes the title for the context menu
        Pickupable current = target.GetComponent<Pickupable>();
        if (current) {
            menuGraphics.SetHeader(current.pickupName);
		}
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
