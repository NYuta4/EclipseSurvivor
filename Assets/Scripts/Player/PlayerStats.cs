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

    [Header("Combat")]
    [SerializeField] private int attackPower = 1;
    [SerializeField] private float fireIntervalMultiplier = 1f;

    private bool isDead = false;

    private PlayerController controller;
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private BattleUIController battleUIController;

    public int MaxHp => maxHp;
    public int CurrentHp => currentHp;

    public int CurrentLevel => currentLevel;
    public int CurrentExp => currentExp;
    public int NextLevelExp => nextLevelExp;
    public int AttackPower => attackPower;
    public float FireIntervalMultiplier => fireIntervalMultiplier;



    void Start()
    {
        currentHp = maxHp;

        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        battleUIController = FindObjectOfType<BattleUIController>();
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
        nextLevelExp += 5;

        Debug.Log("Level Up : " + currentLevel);

        if (battleUIController != null)
        {
            battleUIController.ShowLevelUpSelection(this);
        }
    }

    public void UpgradeAttackPower()
    {
        attackPower += 1;
        Debug.Log("Attack Power : " + attackPower);
    }

    public void UpgradeFireRate()
    {
        fireIntervalMultiplier *= 0.9f;
        Debug.Log("Fire Rate Up : " + fireIntervalMultiplier);
    }

    public void UpgradeMaxHp()
    {
        maxHp += 2;
        currentHp += 2;
        Debug.Log("Max HP Up : " + maxHp);
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