using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public int levelToUnlock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Check if the entering object is a player
        {
            // Get the current number of unlocked levels from PlayerPrefs
            int numberOfUnlockedLevels = PlayerPrefs.GetInt("LevelsUnlocked", 1);

            // Check if the next level is the one to unlock
            if (numberOfUnlockedLevels == levelToUnlock - 1)
            {
                // Increment the number of unlocked levels and save it
                PlayerPrefs.SetInt("LevelsUnlocked", levelToUnlock);
                PlayerPrefs.Save();
            }
        }
    }
}
