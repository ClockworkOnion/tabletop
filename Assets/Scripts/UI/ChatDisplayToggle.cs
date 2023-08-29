using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Handles showing and hiding the chat panel, or rather the click
on the button for showing/hiding the panel. </summary> */
public class ChatDisplayToggle : MonoBehaviour
{
    private bool isVisible = true;
    private RectTransform chatTransform;
    public Vector3 visiblePosition;
    public Vector3 hiddenPosition;
    private const float TOGGLE_TIME = 0.5f;

    public void Awake()
    {
        chatTransform = GameObject.Find("ChatWindow").GetComponent<RectTransform>();
        visiblePosition = chatTransform.position;
        hiddenPosition = visiblePosition + new Vector3(400, 0, 0);
    }

    public void ToggleChatDisplayed()
    {
        if (isVisible)
        {
            Debug.Log("Toggling off");
            isVisible = false;
            LeanTween.cancel(chatTransform.gameObject);
            LeanTween.move(chatTransform.gameObject, hiddenPosition, TOGGLE_TIME).setEaseInOutCubic();
            return;
        }

        if (!isVisible)
        { 
            Debug.Log("Toggling on");
            isVisible = true;
            LeanTween.cancel(chatTransform.gameObject);
            LeanTween.move(chatTransform.gameObject, visiblePosition, TOGGLE_TIME).setEaseInOutCubic();
            return;
		}
    }
}
