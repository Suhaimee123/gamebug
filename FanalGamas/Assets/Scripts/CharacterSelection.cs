using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] characters;
	public int selectedCharacter = 0;

	public void NextCharacter()
	{
		characters[selectedCharacter].SetActive(false);
		selectedCharacter = (selectedCharacter + 1) % characters.Length;
		characters[selectedCharacter].SetActive(true);
	}

	public void PreviousCharacter()
	{
		characters[selectedCharacter].SetActive(false);
		selectedCharacter--;
		if (selectedCharacter < 0)
		{
			selectedCharacter += characters.Length;
		}
		characters[selectedCharacter].SetActive(true);
	}

	public void StartGame()
	{	
		PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
		// int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		// Debug.Log("Loading next scene: " + nextSceneIndex);
		// SceneManager.LoadSceneAsync(nextSceneIndex);
	}

}