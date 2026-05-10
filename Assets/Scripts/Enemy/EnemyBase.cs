using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject expGemPrefab;

    private int currentHp;
    private Transform player;
    private Rigidbody2D rb;
    private bool isDead = false;

    private float nextDamageTime = 0f;
    [SerializeField] private float damageInterval = 1f;

    void Start()
    {
        if (enemyData == null)
        {
            Debug.LogError("EnemyData Ç™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
            return;
        }

        currentHp = enemyData.maxHp;

        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null || isDead) return;

        Vector2 dir = (player.position - transform.position).normalized;

        rb.velocity = dir * enemyData.moveSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDead) return;

        PlayerStats playerStats = other.GetComponentInParent<PlayerStats>();

        if (playerStats == null) return;

        if (Time.time < nextDamageTime) return;

        playerStats.TakeDamage(enemyData.contactDamage);

        nextDamageTime = Time.time + damageInterval;

        Debug.Log("Player Damage");
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        GameManager.Instance.CurrentRun.killCount++;

        DropExp();

        rb.velocity = Vector2.zero;
        rb.simulated = false;

        Collider2D enemyCollider = GetComponent<Collider2D>();

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        Destroy(gameObject);
    }

    private void DropExp()
    {
        if (expGemPrefab == null) return;

        GameObject expGem = Instantiate(
            expGemPrefab,
            transform.position,
            Quaternion.identity
        );

        ExperienceGem gem = expGem.GetComponent<ExperienceGem>();

        if (gem != null)
        {
            gem.SetExpValue(enemyData.expDropValue);
        }
    }
}