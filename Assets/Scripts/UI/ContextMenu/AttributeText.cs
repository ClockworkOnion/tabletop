using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttributeText : MonoBehaviour
{
    private Actor actor;
    private string attributeName;

    public void SetNameAndActor(Actor _actor, string _attributeName) {
        actor = _actor;
        attributeName = _attributeName;
    }

    public void DecreaseAttribute() {
        float previousValue = actor.attributes[attributeName];
        actor.attributes[attributeName] = previousValue - 1;
        GetComponentInChildren<TextMeshProUGUI>().SetText(attributeName + " : " + (previousValue - 1));
    }

    public void IncreaseAttribute() { 
        float previousValue = actor.attributes[attributeName];
        actor.attributes[attributeName] = previousValue + 1;
        GetComponentInChildren<TextMeshProUGUI>().SetText(attributeName + " : " + (previousValue + 1));
    }
}
