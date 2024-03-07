using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureImageSelect : MonoBehaviour
{
    public GameObject fileSelectPrefab; // Set in inspector
    private FileReaderWriter fileReader;
    public GameObject targetActor;
    public TextureLoader targetTexLoader;

    void Start()
    {
        FileReaderWriter fileReader = GetComponent<FileReaderWriter>();
        List<string> fileList = fileReader.GetFileNames();

        // Create a button for each file
        foreach (string fileName in fileList)
        {
            if (!fileName.EndsWith("png"))
                continue;

            GameObject newObject = Instantiate(fileSelectPrefab);
            newObject.transform.SetParent(transform);
            FileButton newButton = newObject.GetComponent<FileButton>();
            newButton.SetData(fileName, targetTexLoader, this.gameObject);
        }
    }
}
