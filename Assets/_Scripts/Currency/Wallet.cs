using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int startingGold;
    private int currentGold;

    private const string GoldPlayerPrefsKey = "PlayerGold";

    private void Start()
    {
        LoadGold();
    }

    public int GetCurrentGold()
    {
        return currentGold;
    }

    public void SetCurrentGold(int gold)
    {
        currentGold = gold;
        SaveGold();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        SaveGold();
    }

    public void SubtractGold(int amount)
    {
        currentGold -= amount;
        SaveGold();
    }

    public void ResetToStartingGold()
    {
        currentGold = startingGold;
        SaveGold();
    }

    private void LoadGold()
    {
        if (PlayerPrefs.HasKey(GoldPlayerPrefsKey))
        {
            currentGold = PlayerPrefs.GetInt(GoldPlayerPrefsKey);
        }
        else
        {
            currentGold = startingGold;
            SaveGold();
        }
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt(GoldPlayerPrefsKey, currentGold);
        PlayerPrefs.Save();
    }
}
