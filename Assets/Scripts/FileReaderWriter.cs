using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReaderWriter : MonoBehaviour
{
    public List<string> GetLinesFromFile(string fileName)
    {
        List<string> textLines = new List<string>();
        string basePath = Application.persistentDataPath;
        string fullPath = basePath + "/" + fileName;
        string json = ReadStringFromFile(fullPath);
        ActorJSON toRead = new ActorJSON();
        if (!string.IsNullOrEmpty(json))
        {
            toRead = JsonUtility.FromJson<ActorJSON>(json);
        }

        foreach (string line in toRead.lines)
        {
            textLines.Add(line);
        }
        return textLines;
    }

    public List<string> GetFileNames()
    {
        string path = Application.persistentDataPath;
        string[] fullNames = Directory.GetFiles(path);
        List<string> fileNames = new List<string>();
        foreach (string name in fullNames)
        {
            int lastForwardSlashPos = name.LastIndexOf('/') + 1;
            string fileName = name.Substring(lastForwardSlashPos, name.Length - lastForwardSlashPos);
            fileNames.Add(fileName);
        }
        return fileNames;
    }

    private string ReadStringFromFile(string path)
    {
        try
        {
            using StreamReader reader = new(path);
            return reader.ReadToEnd();
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading from file: " + e.Message);
            return null;
        }
    }

    private void WriteStringToFile(string path, string content)
    {
        try
        {
            using StreamWriter writer = new(path);
            writer.Write(content);
        }
        catch (Exception e)
        {
            Debug.LogError("Error writing to file: " + e.Message);
        }
    }

}

public struct ActorJSON
{
    public List<string> lines;
}