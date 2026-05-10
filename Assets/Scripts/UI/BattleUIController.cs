using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pushAnyKey;
    public TextMeshProUGUI timerText;

    void Start()
    {
        gameOverPanel.SetActive(false);
        pushAnyKey.SetActive(false);

        GameManager.Instance.SetGameOverPanel(gameOverPanel);
        GameManager.Instance.SetPushAnyKey(pushAnyKey);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentRun == null) return;

        float time = GameManager.Instance.CurrentRun.surviveTime;

        int minutes = Mathf.FloorToInt(time / 60f);

        int seconds = Mathf.FloorToInt(time % 60f);

        int milliseconds =
            Mathf.FloorToInt((time * 100f) % 100f);

        timerText.text =
            $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public void OnClickStartBattle()
    {
        GameManager.Instance.StartRun();

        SceneManager.LoadScene("Battle");
    }
}