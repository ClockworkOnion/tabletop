using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class TokenOutlineControl : MonoBehaviour
{
    private Outline outline;
    public bool outlineOnHover = true;

    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        if (outline)
        {
            outline.enabled = false;
            outline.OutlineColor = new Color(1f, 0.3f, 0.3f);
            outline.OutlineWidth = 4f;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
    }

    private void OnMouseEnter()
    {
        if (outline && outlineOnHover) outline.enabled = true;
    }

    private void OnMouseExit()
    {
        if (outline && outlineOnHover) outline.enabled = false;
    }
}
