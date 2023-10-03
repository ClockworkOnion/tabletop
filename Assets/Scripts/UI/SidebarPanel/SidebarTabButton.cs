using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SidebarTabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler

{
    public GameObject linkedPanel;
    private Image btnImage;
    private SidebarPanel sidebarPanel;
    private bool isActive = false;

    private Color activeColor = new Color(.9f, .4f, .4f, 1f);
    private Color defaultColor = new Color(.51f, .58f, .65f, 1f);
    private Color hoverColor = new Color(.34f, .38f, .45f, 1f);
    private Color hoverActiveColor = new Color(.91f, .52f, .52f, 1f);

    public void Awake()
    {
        btnImage = GetComponent<Image>();
        sidebarPanel = GameObject.Find("SidebarPanel").GetComponent<SidebarPanel>();
        sidebarPanel.Subscribe(this);
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
        if (isActive) return;

        // Disable all panels, but activate this one again.
        sidebarPanel.DisableChildren();
        Activate(true);
    }

    public void Activate(bool status)
    {
        isActive = status;
        btnImage.color = (status) ? hoverActiveColor : defaultColor;
        if (linkedPanel) linkedPanel.SetActive(status);
    }

}
