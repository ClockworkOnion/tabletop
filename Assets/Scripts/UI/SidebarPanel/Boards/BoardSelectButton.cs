using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardSelectButton : PanelButton
{
    public GameObject linkedBoard;
    public override void Start()
    {
        parentPanel = GameObject.Find("BoardsPanel").GetComponent<PlaceableSelectPanel>();
        parentPanel.Subscribe(this);
    }

    public override void Activate(bool status) {
        if (linkedBoard) linkedBoard.SetActive(status);
    }
}
