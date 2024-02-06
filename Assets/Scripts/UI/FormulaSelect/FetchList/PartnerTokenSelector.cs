using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PartnerTokenSelector : MonoBehaviour, FetchListSelector
{
    string attributeToFetch;
    private Actor partnerToken;
    public TextMeshProUGUI label;

    public Texture2D selectCursor;
    public Texture2D hoverValidCursor;
    private Vector2 clickSpot = new Vector2(36, 36);
    private bool selectMode = false;
    public MousePositioning mousePosition;
    public Image inputBG;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    void Start()
    {
        label.text = "Select a token for " + attributeToFetch + "!";
    }

    public void Update()
    {
        if (!selectMode)
        {
            Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
            selectMode = false;
            return;
        }

        if (mousePosition.GetHovered().GetComponent<Actor>() is Actor hoveredActor && hoveredActor.attributes.ContainsKey(attributeToFetch))
        {
            Cursor.SetCursor(hoverValidCursor, clickSpot, CursorMode.Auto);
            if (Input.GetMouseButtonDown(0))
                SetPartnerToken(hoveredActor);
        }
        else
            Cursor.SetCursor(selectCursor, clickSpot, CursorMode.Auto);
    }

    private void SetPartnerToken(Actor newPartner)
    {
        partnerToken = newPartner;
        inputBG.color = new Color(0.52f, 0.91f, 0.52f);
        label.text = "Perform with " + attributeToFetch + " of " + partnerToken.GetComponent<Pickupable>().pickupName;
    }


    public void ToFetch(string fetchString)
    {
        attributeToFetch = fetchString;
    }

    public string GetLabel()
    {
        return "@" + attributeToFetch;
    }

    public void SelectPartner()
    {
        // Set it up so that left-clicking an Actor will make it the partner.
        Cursor.SetCursor(selectCursor, clickSpot, CursorMode.Auto);
        selectMode = true;
    }

    public void OnDestroy()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }

    // Interface methods
    public float SelectedValue()
    {
        if (partnerToken == null)
        {
            Debug.LogError("Tried to evaluate PartnerTokenSelector while no token was selected. How could this happen?!");
            return 0f;
        }
        return partnerToken.attributes[attributeToFetch];
    }

    public bool IsValid() { return partnerToken != null; }
}
