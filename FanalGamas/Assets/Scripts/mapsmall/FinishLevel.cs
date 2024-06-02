using UnityEngine;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private Button finishButton;
    private bool isFinished = false;
    
    private Map currentMap;

    public void Finish(Map _currentMap)
    {
        if (isFinished) return;

        isFinished = true;

        PlayerPrefs.SetInt("currentScene", PlayerPrefs.GetInt("currentScene") + 1);

        // Assign the current map
        currentMap = _currentMap;

        // Check if the current map's levelIndex is 1
        if (currentMap.levelIndex == 1)
        {
            // Hide the finishButton
            if (finishButton != null)
            {
                finishButton.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Finish button is not assigned!");
            }
        }

        // Now you can access the levelIndex of the current map
        Debug.Log("Level Index of the current map: " + currentMap.levelIndex);
    }
}
