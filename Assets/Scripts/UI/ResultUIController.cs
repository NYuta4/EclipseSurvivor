using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultUIController : MonoBehaviour
{
    [Header("Result Text")]
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI surviveTimeText;
    [SerializeField] private TextMeshProUGUI highKillCountText;
    [SerializeField] private TextMeshProUGUI totalKillCountText;

    void Start()
    {
        UpdateResultUI();
    }

    private void UpdateResultUI()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.CurrentRun == null) return;
        if (GameManager.Instance.SaveData == null) return;

        int killCount = GameManager.Instance.CurrentRun.killCount;
        float surviveTime = GameManager.Instance.CurrentRun.surviveTime;
        int highKillCount = GameManager.Instance.SaveData.highKillCount;
        int totalKillCount = GameManager.Instance.SaveData.totalKillCount;

        killCountText.text = $"Kills : {killCount}";
        surviveTimeText.text = $"Time : {FormatTime(surviveTime)}";
        highKillCountText.text = $"Best Kills : {highKillCount}";
        totalKillCountText.text = $"Total Kills : {totalKillCount}";
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public void OnClickBackTitle()
    {
        SceneManager.LoadScene("Title");
    }
}