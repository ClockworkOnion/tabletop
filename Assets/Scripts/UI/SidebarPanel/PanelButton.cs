using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class PanelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    protected Image btnImage;
    protected bool isActive = false;
    protected PlaceableSelectPanel parentPanel;
    protected  Color activeColor = StaticRefs.activeColor;
    protected  Color defaultColor = StaticRefs.defaultColor;
    protected  Color hoverColor = StaticRefs.hoverColor;
    protected  Color hoverActiveColor = StaticRefs.hoverActiveColor;

    void Awake() {
        btnImage = GetComponent<Image>();
    }

    /// <summary>
    /// Inheriting classes must get a reference to their parentPanel and subscribe to it here.
    /// </summary>
    public abstract void Start(); 

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
        parentPanel.DisableChildren();
        Activate(true);
    }

    /// <summary>
    /// What happens when the button is clicked, or when
    /// it becomes deselected because another button was clicked.
    /// </summary>
    /// <param name="status"></param>
    public abstract void Activate(bool status);
}
