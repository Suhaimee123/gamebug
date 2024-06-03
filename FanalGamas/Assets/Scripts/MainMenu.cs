using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour


{
    private void Start()
    {
        // สร้างการฟังก์ชันสำหรับจับเหตุการณ์เมื่อมีการเปลี่ยนซีน
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        // ถ้าเปลี่ยนซีนให้บันทึกเกมโดยอัตโนมัติ
        SaveGame();
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/Save/savegame.txt";
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            SceneManager.LoadScene(data);
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }

 public void NewGame()
    {
        string path = Application.persistentDataPath + "/Save/";
        PlayerPrefs.DeleteAll();
        
        string[] filesToDelete = { "savegame.txt", "savename.txt", "savelevel.txt" };

        foreach (string fileName in filesToDelete)
        {
            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log(fileName + " file deleted.");
            }
        }

        SceneManager.LoadScene("Choose a character");
    }


      public void SaveGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // ตรวจสอบว่าซีนปัจจุบันไม่ใช่ "Main Menu"
        if (currentScene != "Main Menu")
        {
            string savePath = Path.Combine(Application.persistentDataPath, "Save", "savegame.txt");

            // สร้างโฟลเดอร์ Save ถ้ายังไม่มี
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            // เขียนชื่อซีนปัจจุบันลงในไฟล์
            File.WriteAllText(savePath, currentScene);

            Debug.Log("Game saved.");
        }
        else
        {
            Debug.LogWarning("Cannot save in Main Menu.");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Loading next scene: " + nextSceneIndex);
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }

        public void Back(string sceneName)
    {   
        SceneManager.LoadSceneAsync(sceneName);
    }
}
