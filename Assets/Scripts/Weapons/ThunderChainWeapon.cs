using System.Collections.Generic;
using UnityEngine;

public class ThunderChainWeapon : WeaponBase
{
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private int chainCount = 3;
    [SerializeField] private float searchRadius = 6f;
    [SerializeField] private float chainRadius = 3f;

    protected override void Attack()
    {
        EnemyBase firstEnemy = FindNearestEnemy(transform.position, searchRadius, null);

        if (firstEnemy == null) return;

        List<EnemyBase> hitEnemies = new List<EnemyBase>();

        EnemyBase currentEnemy = firstEnemy;

        for (int i = 0; i < chainCount; i++)
        {
            if (currentEnemy == null) break;

            currentEnemy.TakeDamage(GetDamage());

            hitEnemies.Add(currentEnemy);

            currentEnemy = FindNearestEnemy(
                currentEnemy.transform.position,
                chainRadius,
                hitEnemies
            );
        }
    }

    private int GetDamage()
    {
        if (playerStats == null)
        {
            return baseDamage;
        }

        return baseDamage + playerStats.AttackPower - 1;
    }

    private EnemyBase FindNearestEnemy(
        Vector3 center,
        float radius,
        List<EnemyBase> ignoreList
    )
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius);

        EnemyBase nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            EnemyBase enemy = hit.GetComponentInParent<EnemyBase>();

            if (enemy == null) continue;

            if (ignoreList != null && ignoreList.Contains(enemy)) continue;

            float distance = Vector2.Distance(center, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}