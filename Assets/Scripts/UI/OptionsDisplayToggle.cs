using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsDisplayToggle : MonoBehaviour
{
    private bool isVisible = true;
    private RectTransform optionsTransform;
    private Vector3 visiblePosition;
    private Vector3 hiddenPosition;

    public void Awake()
    {
        optionsTransform = GameObject.Find("OptionsWindow").GetComponent<RectTransform>();
        visiblePosition = optionsTransform.position;
        hiddenPosition = visiblePosition + new Vector3(-200, 0, 0);
    }

    public void ToggleChatDisplayed()
    {
        if (isVisible)
        {
            Debug.Log("Toggling off");
            isVisible = false;
            LeanTween.cancel(optionsTransform.gameObject);
            LeanTween.move(optionsTransform.gameObject, hiddenPosition, 1f).setEaseInOutCubic();
            return;
        }

        if (!isVisible)
        { 
            Debug.Log("Toggling on");
            isVisible = true;
            LeanTween.cancel(optionsTransform.gameObject);
            LeanTween.move(optionsTransform.gameObject, visiblePosition, 1f).setEaseInOutCubic();
            return;
		}
    }
}
