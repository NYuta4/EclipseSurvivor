using UnityEngine;

public class OrbitWeapon : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private int damage = 1;

    private float angle;

    void Start()
    {
        if (center == null)
        {
            center = transform.parent;
        }
    }

    void Update()
    {
        angle += rotateSpeed * Time.deltaTime;

        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0f
        ) * radius;

        transform.position = center.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponentInParent<EnemyBase>();

        if (enemy == null) return;

        enemy.TakeDamage(damage);
    }
}