using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponShopController : MonoBehaviour
{
    [Header("Core UI")]
    [SerializeField] private TextMeshProUGUI coreText;

    [Header("Weapon List")]
    [SerializeField] private List<WeaponData> allWeaponDataList = new List<WeaponData>();

    [Header("Shop Slots")]
    [SerializeField] private List<WeaponShopItemUI> shopItemSlots = new List<WeaponShopItemUI>();

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        UpdateCoreText();

        List<WeaponData> purchasableWeapons = GetPurchasableWeapons();

        for (int i = 0; i < shopItemSlots.Count; i++)
        {
            if (i < purchasableWeapons.Count)
            {
                shopItemSlots[i].Setup(purchasableWeapons[i], this);
            }
            else
            {
                shopItemSlots[i].SetComingSoon();
            }
        }
    }

    private void UpdateCoreText()
    {
        if (coreText == null) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.SaveData == null) return;

        coreText.text = $"Core : {GameManager.Instance.SaveData.core}";
    }

    private List<WeaponData> GetPurchasableWeapons()
    {
        List<WeaponData> result = new List<WeaponData>();

        foreach (WeaponData weaponData in allWeaponDataList)
        {
            if (weaponData == null) continue;

            bool unlocked = GameManager.Instance.IsWeaponUnlocked(weaponData);

            if (!unlocked)
            {
                result.Add(weaponData);
            }
        }

        return result;
    }
}