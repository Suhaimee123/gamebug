using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    public TMP_Text label;
    
    public static GameObject activeCharacter; // Add this line

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefabs[selectedCharacter];
        activeCharacter = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        label.text = prefab.name;
    }
}
