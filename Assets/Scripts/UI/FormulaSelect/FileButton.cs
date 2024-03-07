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
    private TextureLoader texLoader;
    private FileReaderWriter fileReader;
    private FormulaFileSelector parent;

    private bool isTextureLoader = false;
    private GameObject texloaderParent;

    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().SetText(fileName);
        fileReader = GetComponent<FileReaderWriter>();
    }

    // Practically a constructor to pass along data... Instantiate() doesn't do it otherwise
    public void SetData(string name, GameObject target, FormulaFileSelector _parent)
    {
        fileName = name;
        targetObject = target;
        parent = _parent;
    }

    public void SetData(string name, TextureLoader loader, GameObject _parent)
    {
        fileName = name;
        texLoader = loader;
        isTextureLoader = true;
        texloaderParent = _parent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isTextureLoader)
        {
            texLoader.LoadFromImage(fileName);
            // Clean up
            TooltipSystem.Hide();
            Destroy(texloaderParent);
        }
        else // Load actor
        {
            List<string> lines = fileReader.GetLinesFromFile(fileName);
            // Create actor and pass along data from the file
            Actor newActor = targetObject.AddComponent(typeof(Actor)) as Actor;
            newActor.ParseFormula(lines);

            // Clean up
            TooltipSystem.Hide();
            Destroy(parent.gameObject);
        }


    }
}