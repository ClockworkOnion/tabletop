using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On the parent UI element for the select buttons for placeable items.
/// It mostly handles keeping track of the buttons and
/// deactivation of other buttons when a new one is pressed.
/// </summary>
public class PlaceableSelectPanel : MonoBehaviour
{
    public static GameObject selectedObject;

    [SerializeField]
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
