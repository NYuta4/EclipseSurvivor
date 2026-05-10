using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed = 2f;

    [Header("HP")]
    public int maxHp = 3;
    private int currentHp;

    [SerializeField]
    [Header("ExpObj")]
    private GameObject expGemPrefab;

    private Transform player;
    private Rigidbody2D rb;

    private bool isDead = false;

    void Start()
    {
        currentHp = maxHp;

        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null || isDead) return;

        Vector2 dir = (player.position - transform.position).normalized;

        rb.velocity = dir * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject
                .GetComponent<PlayerStats>()
                .TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;

        if (currentHp <= 0)
        {
            Instantiate(
                expGemPrefab,
                transform.position,
                Quaternion.identity
            );
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        GameManager.Instance.CurrentRun.killCount++;
        Debug.Log(GameManager.Instance.CurrentRun.killCount);

        rb.velocity = Vector2.zero;
        rb.simulated = false;

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject);
    }
}