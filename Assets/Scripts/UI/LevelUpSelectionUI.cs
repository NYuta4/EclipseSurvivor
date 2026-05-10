using UnityEngine;

public class LevelUpSelectionUI : MonoBehaviour
{
    private PlayerStats targetPlayer;

    public void Show(PlayerStats player)
    {
        targetPlayer = player;

        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickAttackPower()
    {
        targetPlayer.UpgradeAttackPower();
        Close();
    }

    public void OnClickFireRate()
    {
        targetPlayer.UpgradeFireRate();
        Close();
    }

    public void OnClickMaxHp()
    {
        targetPlayer.UpgradeMaxHp();
        Close();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}