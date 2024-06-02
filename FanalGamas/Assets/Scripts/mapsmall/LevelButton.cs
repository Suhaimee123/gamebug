using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string sceneName; // สร้างตัวแปรชื่อซีน

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneName); // โหลดซีนโดยใช้ชื่อซีน
    }
}
