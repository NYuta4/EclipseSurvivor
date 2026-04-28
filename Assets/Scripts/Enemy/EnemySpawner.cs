using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, 2f);
    }

    void SpawnEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-8, 8), Random.Range(-5, 5), 0);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}