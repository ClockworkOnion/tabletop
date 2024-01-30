using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartnerTokenSelector : MonoBehaviour, FetchListSelector
{
    string attributeToFetch;
    public TextMeshProUGUI label;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public float SelectedValue()
    {
        Debug.LogError("PartnerTokenSelector not yet implemented. Returning 0");
        return 0;
    }

    public bool IsValid() { return false; }

    void Start()
    {
        label.text = "Select token for " + attributeToFetch + "!";
    }

    public void ToFetch(string fetchString)
    {
        attributeToFetch = fetchString;
    }

    public string GetLabel() {
        return "@" + attributeToFetch;
    }
}
