using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseUIController : MonoBehaviour
{
    public void OnClickStartBattle()
    {
        SceneManager.LoadScene("Battle");
    }
    public void OnClickBack()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnClickUpgradeHP()
    {
        Debug.Log("HP Up");
    }
    public void OnClickUpgradeAtk()
    {
        Debug.Log("Atk Up");
    }
    public void OnClickUpgradeSpeed()
    {
        Debug.Log("Speed Up");
    }
}