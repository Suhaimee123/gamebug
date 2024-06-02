using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Objects/Map")]
public class Map : ScriptableObject
{
    public string mapName;
    public Color nameColor;
    public string mapDescription;
    public Sprite mapImage;
    public int levelIndex;
}
