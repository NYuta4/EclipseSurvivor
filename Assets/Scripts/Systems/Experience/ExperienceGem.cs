using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [SerializeField] private int expValue = 1;

    public void SetExpValue(int value)
    {
        expValue = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats player = other.GetComponentInParent<PlayerStats>();

        if (player == null) return;

        player.AddExp(expValue);

        Destroy(gameObject);
    }
}