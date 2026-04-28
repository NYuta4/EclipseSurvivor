using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void Init()
    {
        Debug.Log("GameManager Init");
    }

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
    }
}