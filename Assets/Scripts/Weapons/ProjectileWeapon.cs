using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private GameObject bulletPrefab;

    protected override void Attack()
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