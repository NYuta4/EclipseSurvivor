using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public RunData CurrentRun { get; private set; }
    public SaveData SaveData { get; private set; }

    public static GameManager Instance;

    private GameObject gameOverPanel;
    private GameObject pushAnyKey;

    public void Init()
    {
        Debug.Log("GameManager Init");
    }

    void Update()
    {
        if (CurrentRun == null) return;

        CurrentRun.surviveTime += Time.deltaTime;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SaveData = new SaveData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartRun()
    {
        CurrentRun = new RunData();

        CurrentRun.killCount = 0;
        CurrentRun.surviveTime = 0f;
        CurrentRun.level = 1;
    }

    public bool IsWeaponUnlocked(WeaponData weaponData)
    {
        if (weaponData == null) return false;
        if (SaveData == null) return false;
        if (SaveData.unlockedWeaponIds == null) return false;

        return SaveData.unlockedWeaponIds.Contains(weaponData.weaponId);
    }

    public bool TryUnlockWeapon(WeaponData weaponData)
    {
        if (weaponData == null) return false;
        if (SaveData == null) return false;

        if (SaveData.unlockedWeaponIds == null)
        {
            SaveData.unlockedWeaponIds = new List<string>();
        }

        if (IsWeaponUnlocked(weaponData))
        {
            Debug.Log($"{weaponData.weaponName} is already unlocked");
            return false;
        }

        if (SaveData.core < weaponData.price)
        {
            Debug.Log("Not enough core");
            return false;
        }

        SaveData.core -= weaponData.price;
        SaveData.unlockedWeaponIds.Add(weaponData.weaponId);

        SaveManager.Instance.Save();

        Debug.Log($"{weaponData.weaponName} unlocked");

        return true;
    }

    public void SetGameOverPanel(GameObject panel)
    {
        gameOverPanel = panel;
    }

    public void SetPushAnyKey(GameObject obj)
    {
        pushAnyKey = obj;
    }

    public void SetSaveData(SaveData data)
    {
        SaveData = data;
    }

    public void UpdateRunResult()
    {
        SaveData.totalKillCount += CurrentRun.killCount;

        if (CurrentRun.killCount > SaveData.highKillCount)
        {
            SaveData.highKillCount = CurrentRun.killCount;
        }

        SaveData.core += CurrentRun.killCount;
    }

    public void GameOver()
    {
        UpdateRunResult();

        SaveManager.Instance.Save();

        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        pushAnyKey.SetActive(true);

        yield return new WaitUntil(() => Input.anyKeyDown); 

        SaveData.totalKillCount += CurrentRun.killCount;

        Time.timeScale = 1f;

        SceneManager.LoadScene("Result");
    }
}