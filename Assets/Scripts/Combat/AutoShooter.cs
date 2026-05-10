using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float baseFireInterval = 1f;

    private float fireTimer;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        fireTimer = baseFireInterval;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = GetCurrentFireInterval();
        }
    }

    private float GetCurrentFireInterval()
    {
        if (playerStats == null)
        {
            return baseFireInterval;
        }

        return baseFireInterval * playerStats.FireIntervalMultiplier;
    }

    private void Fire()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy == null) return;

        Vector3 dir = (enemy.transform.position - transform.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(dir);
            bulletScript.SetDamage(playerStats.AttackPower);
        }
    }
}