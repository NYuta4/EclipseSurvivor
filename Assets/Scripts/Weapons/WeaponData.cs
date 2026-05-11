using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponId;
    public string weaponName;

    [TextArea]
    public string description;

    public int price = 10;

    public GameObject weaponPrefab;
    public Sprite icon;
}