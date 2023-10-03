using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidebarPanel : MonoBehaviour
{

    [SerializeField]
    private List<SidebarTabButton> tabButtons = new List<SidebarTabButton>();

    public void Subscribe(SidebarTabButton button)
    {
        tabButtons.Add(button);
    }

    public void DisableChildren()
    {
        tabButtons.ForEach((btn) => btn.Activate(false));
    }

}
