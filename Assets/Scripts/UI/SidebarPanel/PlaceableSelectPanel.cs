using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On the parent UI element for the select buttons for placeable items.
/// It mostly handles keeping track of the buttons and
/// deactivation of other buttons when a new one is pressed.
/// TODO: Rename this to "Sidebar Select Panel" or something. It can be
/// used on every sidebar panel, not specific to the placeable panel
/// </summary>
public class PlaceableSelectPanel : MonoBehaviour
{
    public static GameObject selectedObject;
    private List<PlaceableSelect> placeButtons = new List<PlaceableSelect>();

    public void Subscribe(PlaceableSelect button)
    {
        placeButtons.Add(button);
    }

    public void DisableChildren()
    {
        placeButtons.ForEach((btn) => btn.Activate(false));
    }

}
