using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Vector3 dir;

    public void SetDirection(Vector3 value)
    {
        dir = value;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}