using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.persistentDataPath + "/save.json";
    }

    public void Save()
    {
        string json =
            JsonUtility.ToJson(GameManager.Instance.SaveData, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Save Complete");
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No Save File");

            return;
        }

        string json = File.ReadAllText(savePath);

        GameManager.Instance.SetSaveData(
            JsonUtility.FromJson<SaveData>(json)
        );

        Debug.Log("Load Complete");
    }
}