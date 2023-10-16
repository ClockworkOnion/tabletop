using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int hoveredUIElements = 0;
    private DebugText debugText;

    public Dictionary<string, GameObject> prefabMap;
    public PrefabDict prefabs;

    // Start is called before the first frame update
    void Start()
    {
        debugText = GameObject.Find("DebugText").GetComponent<DebugText>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.DisplayText("Hovered UI Elements: " + hoveredUIElements.ToString());
    }


    public void SetHoveredElements(int change)
    {
        if (hoveredUIElements == 0 && change < 0)
        {
            Debug.LogError("UI Hovering Error! (Forgot to log an OnPointerEnter?");
        }
        hoveredUIElements += change;
    }

}
