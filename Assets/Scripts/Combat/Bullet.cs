using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;

    private Vector3 dir;
    private int damage = 1;

    public void SetDirection(Vector3 value)
    {
        dir = value.normalized;
        Destroy(gameObject, 3f);
    }

    public void SetDamage(int value)
    {
        damage = value;
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponentInParent<EnemyBase>();

        if (enemy == null) return;

        Debug.Log("hit");

        enemy.TakeDamage(damage);

        Destroy(gameObject);
    }
}