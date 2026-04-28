using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHp = 10;
    public int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}