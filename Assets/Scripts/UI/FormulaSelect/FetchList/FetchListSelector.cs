using System;
using UnityEngine;
public interface FetchListSelector
{
    float SelectedValue();
    bool IsValid();
    void ToFetch(string fetchString);
    string GetLabel();
    GameObject GetGameObject();
}

