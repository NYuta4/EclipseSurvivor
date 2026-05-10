using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("HP")]
    public int maxHp = 10;
    public int currentHp; 

    [Header("Level")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int currentExp = 0;
    [SerializeField] private int nextLevelExp = 5;

    private bool isDead = false;

    private PlayerController controller;
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    public int MaxHp => maxHp;
    public int CurrentHp => currentHp;

    public int CurrentLevel => currentLevel;
    public int CurrentExp => currentExp;
    public int NextLevelExp => nextLevelExp;



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

    public void AddExp(int value)
    {
        if (isDead) return;

        currentExp += value;

        while (currentExp >= nextLevelExp)
        {
            currentExp -= nextLevelExp;
            LevelUp();
        }
    }


    private void LevelUp()
    {
        currentLevel++;

        currentExp = 0;

        nextLevelExp += 5;

        Debug.Log("Level Up!");
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