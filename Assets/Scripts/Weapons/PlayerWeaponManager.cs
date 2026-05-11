using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("Weapon Parent")]
    [SerializeField] private Transform weaponParent;

    [Header("All Weapon Data")]
    [SerializeField] private List<WeaponData> allWeaponDataList = new List<WeaponData>();

    private readonly List<WeaponData> ownedWeapons = new List<WeaponData>();

    void Start()
    {
        EquipUnlockedWeapons();
    }

    public void EquipUnlockedWeapons()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.SaveData == null) return;

        foreach (WeaponData weaponData in allWeaponDataList)
        {
            if (weaponData == null) continue;

            bool unlocked = GameManager.Instance.IsWeaponUnlocked(weaponData);

            if (!unlocked) continue;

            AddWeapon(weaponData);
        }
    }

    public void AddWeapon(WeaponData weaponData)
    {
        if (weaponData == null)
        {
            Debug.LogWarning("WeaponData is null");
            return;
        }

        if (weaponData.weaponPrefab == null)
        {
            Debug.LogWarning($"{weaponData.weaponName} ÇÃPrefabÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
            return;
        }

        if (ownedWeapons.Contains(weaponData))
        {
            Debug.Log($"{weaponData.weaponName} ÇÕÇ∑Ç≈Ç…ëïîıçœÇ›Ç≈Ç∑");
            return;
        }

        ownedWeapons.Add(weaponData);

        Transform parent = weaponParent != null ? weaponParent : transform;

        Instantiate(
            weaponData.weaponPrefab,
            transform.position,
            Quaternion.identity,
            parent
        );

        Debug.Log($"{weaponData.weaponName} Equipped");
    }

    public bool HasWeapon(WeaponData weaponData)
    {
        return ownedWeapons.Contains(weaponData);
    }
}