using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject wolfPrefab;
    [SerializeField] private GameObject tankPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float baseSpawnInterval = 2f;
    [SerializeField] private float spawnRadius = 10f;

    private float spawnTimer;

    void Start()
    {
        spawnTimer = baseSpawnInterval;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentRun == null) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();

            spawnTimer = GetCurrentSpawnInterval();
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefab = GetEnemyPrefabByTime();

        if (prefab == null) return;

        Vector3 spawnPosition = GetSpawnPosition();

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    private GameObject GetEnemyPrefabByTime()
    {
        float time = GameManager.Instance.CurrentRun.surviveTime;

        if (time < 30f)
        {
            return slimePrefab;
        }

        if (time < 60f)
        {
            return Random.value < 0.7f ? slimePrefab : wolfPrefab;
        }

        if (time < 120f)
        {
            float r = Random.value;

            if (r < 0.5f) return slimePrefab;
            if (r < 0.85f) return wolfPrefab;

            return tankPrefab;
        }

        {
            float r = Random.value;

            if (r < 0.35f) return slimePrefab;
            if (r < 0.75f) return wolfPrefab;

            return tankPrefab;
        }
    }

    private float GetCurrentSpawnInterval()
    {
        float time = GameManager.Instance.CurrentRun.surviveTime;

        float interval = baseSpawnInterval - (time / 120f);

        return Mathf.Clamp(interval, 0.4f, baseSpawnInterval);
    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;

        return playerPosition + new Vector3(randomCircle.x, randomCircle.y, 0f);
    }
}