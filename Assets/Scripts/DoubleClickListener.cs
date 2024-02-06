using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleClickListener : MonoBehaviour
{
    private float doubleClickTimer = 0f;

    public ClickEvent doubleClicked;
    public ClickEvent onceClicked;
    public PlayerNetworkHandler clickingPlayer;

    void Update()
    {

        if (doubleClickTimer > 0)
        {
            doubleClickTimer -= Time.deltaTime;
        }
    }

    public void OnMouseDown()
    {
        // Single click
        onceClicked.Invoke(clickingPlayer);

        // Double click
        if (doubleClickTimer <= 0)
        {
            doubleClickTimer = 0.5f;
            return;
        }
        doubleClicked.Invoke(clickingPlayer);
    }

    public void SetNetworkHandler(PlayerNetworkHandler clicker)
    {
        if (clickingPlayer == null)
        {
            clickingPlayer = clicker;
            Debug.Log("Set networkhandler");
            Debug.Log(clickingPlayer);
        }
    }
}

[System.Serializable]
public class ClickEvent : UnityEvent<PlayerNetworkHandler> { }