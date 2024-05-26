using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SingleLevel : MonoBehaviour
{
    private int currentStarsNum = 0;
    public int levelIndex;

    private Dictionary<int, int> levelData = new Dictionary<int, int>(); // Store level data in memory

    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "Save", "savelevel.txt");
        LoadLevelData();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("00_Level Selection");
    }

    public void PressStarsButton(int _starsNum)
    {
        currentStarsNum = _starsNum;

        // Check if levelIndex already exists in the file
        if (!levelData.ContainsKey(levelIndex))
        {
            // If levelIndex doesn't exist, or if currentStarsNum is greater than the existing value, update PlayerPrefs and write to file
            if (currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
            {
                PlayerPrefs.SetInt("Lv" + levelIndex, currentStarsNum);
                string newData = levelIndex.ToString() + "|" + currentStarsNum.ToString();
                AppendDataToFile(newData);
                levelData[levelIndex] = currentStarsNum; // Update levelData in memory
            }
        }
        else
        {
            // If levelIndex exists, retrieve the existing currentStarsNum
            int existingStarsNum = levelData[levelIndex];
            // If currentStarsNum is greater than the existing value, update PlayerPrefs and write to file
            if (currentStarsNum > existingStarsNum)
            {
                PlayerPrefs.SetInt("Lv" + levelIndex, currentStarsNum);
                UpdateStarsNumForLevel(levelIndex, currentStarsNum);
                levelData[levelIndex] = currentStarsNum; // Update levelData in memory
            }
        }

        Debug.Log("Level: " + levelIndex + ", Stars: " + currentStarsNum);

        BackButton();
    }

    private void LoadLevelData()
    {
        if (File.Exists(saveFilePath))
        {
            string[] lines = File.ReadAllLines(saveFilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                int index = int.Parse(parts[0]);
                int stars = int.Parse(parts[1]);
                if (!levelData.ContainsKey(index))
                {
                    levelData.Add(index, stars);
                }
                else
                {
                    levelData[index] = stars;
                }
            }
        }
    }

    private void AppendDataToFile(string newData)
    {
        using (StreamWriter writer = File.AppendText(saveFilePath))
        {
            writer.WriteLine(newData);
        }
    }

    private void UpdateStarsNumForLevel(int levelIndex, int starsNum)
    {
        List<string> updatedLines = new List<string>();
        string[] lines = File.ReadAllLines(saveFilePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            int index = int.Parse(parts[0]);
            if (index == levelIndex)
            {
                updatedLines.Add(levelIndex.ToString() + "|" + starsNum.ToString());
            }
            else
            {
                updatedLines.Add(line);
            }
        }
        File.WriteAllLines(saveFilePath, updatedLines.ToArray());
    }
}
