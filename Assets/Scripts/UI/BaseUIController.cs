using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BaseUIController : MonoBehaviour
{
    [Header("Core UI")]
    [SerializeField] private TextMeshProUGUI coreText;

    [Header("Weapon Price")]
    [SerializeField] private int flameRingPrice = 10;
    [SerializeField] private int thunderChainPrice = 20;

    void Start()
    {
        UpdateUI();
    }

    public void OnClickStartBattle()
    {
        GameManager.Instance.StartRun();

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

    private void UpdateUI()
    {
        if (coreText != null)
        {
            coreText.text = $"Core : {GameManager.Instance.SaveData.core}";
        }
    }
}