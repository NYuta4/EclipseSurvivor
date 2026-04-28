using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Base");
    }

    public void OnClickContinue()
    {
        SceneManager.LoadScene("Base");
    }

    public void OnClickSettings()
    {
        Debug.Log("Open Settings");
    }

    public void OnClickExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}