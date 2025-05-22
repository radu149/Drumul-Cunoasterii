using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject optionsMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void ToggleOptionsMenu(bool show)
    {
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(show);
            Cursor.visible = show;
            Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void SavePlayerData(string playerName, int coins, float playTime, int levelsCompleted)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("CoinsCount", coins);
        PlayerPrefs.SetFloat("PlayTime", playTime);
        PlayerPrefs.SetInt("LevelsCompleted", levelsCompleted);
        PlayerPrefs.Save();
    }

    public void LoadPlayerData(out string playerName, out int coins, out float playTime, out int levelsCompleted)
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        coins = PlayerPrefs.GetInt("CoinsCount", 0);
        playTime = PlayerPrefs.GetFloat("PlayTime", 0f);
        levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted", 0);
    }

    public void ClearPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }
}
