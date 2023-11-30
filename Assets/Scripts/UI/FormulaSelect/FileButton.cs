using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Individual FileButtons are created for each respective file in the persistent data directory
/// </summary>
public class FileButton : MonoBehaviour, IPointerClickHandler
{
    private string fileName;
    private GameObject targetObject;
    private FileReaderWriter fileReader;
    private FormulaFileSelector parent;

    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().SetText(fileName);
        fileReader = GetComponent<FileReaderWriter>();
    }

    public void SetData(string name, GameObject target, FormulaFileSelector _parent) {
        fileName = name;
        targetObject = target;
        parent = _parent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(fileName);
        Actor newActor = targetObject.AddComponent(typeof(Actor)) as Actor;
        List<string> lines = fileReader.GetLinesFromFile(fileName);
        newActor.contents = lines[0];
        TooltipSystem.Hide();
        Destroy(parent.gameObject);
    }
} 