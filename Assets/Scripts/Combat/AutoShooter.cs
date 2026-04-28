using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireInterval = 1f;

    void Start()
    {
        InvokeRepeating(nameof(Fire), 1f, fireInterval);
    }

    void Fire()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy == null) return;

        Vector3 dir = (enemy.transform.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}