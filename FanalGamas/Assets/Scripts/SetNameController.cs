using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class SetNameController : MonoBehaviour
{
    public GameObject Recheck; // Reference to your GameObject in the Unity Editor
    public TMP_InputField userInputField;
    public TMP_Text userName;

    void Start()
    {
        LoadUserName();
    }


    public void setName()
    {
        string inputText = userInputField.text.Trim();

        if (!string.IsNullOrEmpty(inputText))
        {
            userName.text = inputText;
            SaveUserName(inputText);
        }
    }

    public void ResetName()
    {
        userName.text = "";
        DeleteSaveFile();
    }

private void SaveUserName(string name)
{
    
    if (string.IsNullOrEmpty(name))
    {
        Debug.LogError("No name entered! Please enter a name before saving.");
        return;
    }

    string path = Path.Combine(Application.persistentDataPath, "Save", "savename.txt");
    try
    {
        File.WriteAllText(path, name);
        Debug.Log("Username saved: " + name);

        // Activate your GameObject here
        Recheck.SetActive(true);

    }
    catch (IOException e)
    {
        Debug.LogError("Error saving username: " + e.Message);
    }

    
}


    private void LoadUserName()
    {
        string path = Application.persistentDataPath + "/Save/savename.txt";
        
        if (File.Exists(path))
        {
            string savedUserName = File.ReadAllText(path);
            userName.text = savedUserName;
        }
        else
        {
            Debug.Log("No savegame file found.");
        }
    }

    private void DeleteSaveFile()
    {
        string path = Application.persistentDataPath + "/Save/savename.txt";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("savename file deleted.");
        }
    }
}
