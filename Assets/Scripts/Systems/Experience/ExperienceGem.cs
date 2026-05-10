using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [SerializeField]
    private int expValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.AddExp(expValue);
            Destroy(gameObject);
        }
    }
}