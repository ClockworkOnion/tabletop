using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ImageLoadButton : MonoBehaviour, IPointerClickHandler
{
    private string fileName;
    private GameObject targetObject;
    private FileReaderWriter fileReader;
    private FormulaFileSelector parent;
    private TextureLoader texLoader;

    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().SetText(fileName);
        fileReader = GetComponent<FileReaderWriter>();
    }

    // Practically a constructor to pass along data... Instantiate() doesn't do it otherwise
    public void SetData(string name, TextureLoader target) {
        fileName = name;
        texLoader = target;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        texLoader.LoadFromImage(fileName);

        // Clean up
        TooltipSystem.Hide();
        Destroy(parent.gameObject);
    }
}
