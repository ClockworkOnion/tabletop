using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FetchDiceSelector : MonoBehaviour, FetchListSelector
{
    TextMeshProUGUI label;
    string rollToListenFor;
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public float SelectedValue()
    {
        Debug.LogError("FetchDiceSelector not yet implemented. Returning 0");
        return 0;
    }

    public bool IsValid() { return false; }

    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        label.text = "Roll " + rollToListenFor + " to fill!";
    }


    public void ToFetch(string fetchString)
    {
        rollToListenFor = fetchString;
    }

    public string GetLabel() {
        return "!" + rollToListenFor;
    }
}
