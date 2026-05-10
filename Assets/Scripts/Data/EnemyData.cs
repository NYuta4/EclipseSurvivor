using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public int maxHp = 3;
    public float moveSpeed = 2f;
    public int contactDamage = 1;
    public int expDropValue = 1;

    public Color color = Color.white;
}