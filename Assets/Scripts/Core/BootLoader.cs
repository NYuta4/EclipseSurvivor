using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.Init();
        SaveManager.Instance.Load();
        AudioManager.Instance.Init();

        SceneManager.LoadScene("Title");
    }
}