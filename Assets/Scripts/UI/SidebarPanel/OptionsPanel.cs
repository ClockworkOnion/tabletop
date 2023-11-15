using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{

    public Slider scrollSensitivity;
    public Slider panSensitivity;
    public Slider lookSensitivity;

    void Start()
    {
        scrollSensitivity.value = PlayerPrefs.GetFloat("scrollSensitivity");
        panSensitivity.value = PlayerPrefs.GetFloat("panSensitivity");
        lookSensitivity.value = PlayerPrefs.GetFloat("lookSensitivity");


    }

    public void ScrollSensitivityChange() {
        PlayerPrefs.SetFloat("scrollSensitivity", scrollSensitivity.value);
    }

    public void PanSensitivityChange() { 
        PlayerPrefs.SetFloat("panSensitivity", panSensitivity.value);
    }

    public void LookSensitivityChange() {
        PlayerPrefs.SetFloat("lookSensitivity", lookSensitivity.value);
    }

    public float GetScrollSensitivity() {
        return scrollSensitivity.value;
    }

    public float GetPanSensitiviy() {
        return panSensitivity.value;
    }

    public float GetLookSensitiviy() {
        return lookSensitivity.value;
    }
}
