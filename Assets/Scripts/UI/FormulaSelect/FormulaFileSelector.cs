using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main component on the panel for selecting files to be attached as formulas
/// </summary>
public class FormulaFileSelector : MonoBehaviour
{
    public GameObject fileSelectPrefab; // Set in inspector
    private FileReaderWriter fileReader;
    public GameObject targetActor;

    void Start()
    {
        FileReaderWriter fileReader = GetComponent<FileReaderWriter>();
        List<string> fileList = fileReader.GetFileNames();

        // Create a button for each file
        foreach (string fileName in fileList)
        {
            if (!fileName.EndsWith("txt"))
                continue;

            GameObject newObject = Instantiate(fileSelectPrefab);
            newObject.transform.SetParent(transform);
            FileButton newButton = newObject.GetComponent<FileButton>();
            newButton.SetData(fileName, targetActor, this);
        }
    }
}
