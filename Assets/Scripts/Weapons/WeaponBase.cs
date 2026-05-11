using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected float attackInterval = 1f;

    protected PlayerStats playerStats;
    private float attackTimer;

    protected virtual void Start()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        attackTimer = attackInterval;
    }

    protected virtual void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = GetAttackInterval();
        }
    }

    protected float GetAttackInterval()
    {
        if (playerStats == null)
        {
            return attackInterval;
        }

        return attackInterval * playerStats.FireIntervalMultiplier;
    }

    protected abstract void Attack();
}