using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockHandler : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    int unlockedLevelsNumber;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", 1);
        }
        unlockedLevelsNumber = PlayerPrefs.GetInt("LevelsUnlocked");

        // Disable all buttons initially
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    private void Update()
    {
        // Update unlocked levels number if it changes dynamically
        unlockedLevelsNumber = PlayerPrefs.GetInt("LevelsUnlocked");

        // Enable buttons up to the unlocked level
        for (int i = 0; i < unlockedLevelsNumber; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
