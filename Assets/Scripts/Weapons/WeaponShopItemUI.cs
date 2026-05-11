using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button buyButton;

    private WeaponData weaponData;
    private WeaponShopController shopController;

    public void Setup(WeaponData data, WeaponShopController controller)
    {
        weaponData = data;
        shopController = controller;

        nameText.text = weaponData.weaponName;
        priceText.text = $"{weaponData.price} Core";
        descriptionText.text = weaponData.description;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnClickBuy);

        gameObject.SetActive(true);
    }

    public void SetComingSoon()
    {
        weaponData = null;

        nameText.text = "Coming soon...";
        priceText.text = "-";
        descriptionText.text = "More weapons will be added.";

        buyButton.onClick.RemoveAllListeners();
        buyButton.interactable = false;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnClickBuy()
    {
        if (weaponData == null) return;

        bool success = GameManager.Instance.TryUnlockWeapon(weaponData);

        if (success)
        {
            shopController.Refresh();
        }
    }
}