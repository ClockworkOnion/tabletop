using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int hoveredUIElements = 0;
    private DebugText debugText;
    private static UIManager instance;
    public List<PrefabDict> prefabs;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        debugText = GameObject.Find("DebugText").GetComponent<DebugText>();
    }

    void Update()
    {
        //debugText.DisplayText("Hovered UI Elements: " + hoveredUIElements.ToString());
    }

    public static UIManager GetInstance() {
        return instance;
    }

    public void SetHoveredElements(int change)
    {
        if (hoveredUIElements == 0 && change < 0)
        {
            Debug.LogError("UI Hovering Error! (Forgot to log an OnPointerEnter?");
        }
        hoveredUIElements += change;
    }

    public bool IsHoveringUI() {
        return hoveredUIElements > 0;
    }

    public GameObject GetPrefabByName(string name)
    {
        GameObject returnObject = null;
        prefabs.ForEach((prefab) =>
        {
            if (prefab.prefabObject.name == name)
            {
                if (returnObject != null) Debug.LogError("Ambiguous prefab naming in list! Forgot to change a name?");
                returnObject = prefab.prefabObject;
            }
        });
        if (returnObject == null) Debug.LogError("Couldn't find prefab of name " + name + " in list! Forgot to add it to the list in UI manager?");
        return returnObject;
    }


}
