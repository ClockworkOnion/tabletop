using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardSelectButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Color activeColor = StaticRefs.activeColor;
    private Color defaultColor = StaticRefs.defaultColor;
    private Color hoverColor = StaticRefs.hoverColor;
    private Color hoverActiveColor = StaticRefs.hoverActiveColor;
    Image btnImage;
    PlaceableSelectPanel boardSelectParent;
    bool isActive = false;

    void Awake() {
        btnImage = GetComponent<Image>();
    }

    void Start()
    {
        boardSelectParent = GameObject.Find("DecoPanel").GetComponent<PlaceableSelectPanel>();
        //boardSelectParent.Subscribe(this);
    }

    void Update()
    {
        
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
        boardSelectParent.DisableChildren();
        Activate(true);
    }

    public void Activate(bool onOff) { 
    }
}
