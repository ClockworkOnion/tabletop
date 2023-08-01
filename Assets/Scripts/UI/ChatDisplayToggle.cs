using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatDisplayToggle : MonoBehaviour
{
    private bool isVisible = true;
    private RectTransform chatTransform;
    public Vector3 visiblePosition;
    public Vector3 hiddenPosition;

    public void Awake()
    {
        chatTransform = GameObject.Find("ChatWindow").GetComponent<RectTransform>();
        visiblePosition = chatTransform.position;
        hiddenPosition = visiblePosition + new Vector3(200, 0, 0);
    }

    public void ToggleChatDisplayed()
    {
        if (isVisible)
        {
            Debug.Log("Toggling off");
            isVisible = false;
            LeanTween.cancel(chatTransform.gameObject);
            LeanTween.move(chatTransform.gameObject, hiddenPosition, 1f).setEaseInOutCubic();
            return;
        }

        if (!isVisible)
        { 
            Debug.Log("Toggling on");
            isVisible = true;
            LeanTween.cancel(chatTransform.gameObject);
            LeanTween.move(chatTransform.gameObject, visiblePosition, 1f).setEaseInOutCubic();
            return;
		}
    }
}
