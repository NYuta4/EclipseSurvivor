using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHp = 10;
    public int currentHp;

    private bool isDead = false;

    private PlayerController controller;
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHp = maxHp;

        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        controller.enabled = false;

        rb.velocity = Vector2.zero;
        rb.simulated = false;

        col.enabled = false;

        spriteRenderer.enabled = false;

        GameManager.Instance.GameOver();
    }
}