using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int totalKillCount;
    public int highKillCount;
    public int highScore;
    public int playerLevel;

    public int core;

    public List<string> unlockedWeaponIds = new List<string>();
}