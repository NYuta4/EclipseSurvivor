using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pushAnyKey;

    [Header("HP UI")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;

    [Header("EXP UI")]
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Timer UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    private PlayerStats playerStats;

    void Start()
    {
        InitializeGameOverUI();
        RegisterGameOverUI();
        FindPlayer();
        UpdateAllUI();
    }

    void Update()
    {
        UpdateAllUI();
    }

    private void InitializeGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (pushAnyKey != null)
        {
            pushAnyKey.SetActive(false);
        }
    }

    private void RegisterGameOverUI()
    {
        if (GameManager.Instance == null) return;

        if (gameOverPanel != null)
        {
            GameManager.Instance.SetGameOverPanel(gameOverPanel);
        }

        if (pushAnyKey != null)
        {
            GameManager.Instance.SetPushAnyKey(pushAnyKey);
        }
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player が見つかりません。Playerタグを確認してください。");
            return;
        }

        playerStats = player.GetComponent<PlayerStats>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats が Player に付いていません。");
        }
    }

    private void UpdateAllUI()
    {
        UpdateHpUI();
        UpdateExpUI();
        UpdateTimerUI();
    }

    private void UpdateHpUI()
    {
        if (playerStats == null) return;

        if (hpSlider != null)
        {
            hpSlider.maxValue = playerStats.MaxHp;
            hpSlider.value = playerStats.CurrentHp;
        }

        if (hpText != null)
        {
            hpText.text = $"{playerStats.CurrentHp}";
        }
    }

    private void UpdateExpUI()
    {
        if (playerStats == null) return;

        if (expSlider != null)
        {
            expSlider.maxValue = playerStats.NextLevelExp;
            expSlider.value = playerStats.CurrentExp;
        }

        if (expText != null)
        {
            expText.text = $"{playerStats.CurrentExp}";
        }

        if (levelText != null)
        {
            levelText.text = $"{playerStats.CurrentLevel}";
        }
    }

    private void UpdateTimerUI()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.CurrentRun == null) return;
        if (timerText == null) return;

        float time = GameManager.Instance.CurrentRun.surviveTime;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}