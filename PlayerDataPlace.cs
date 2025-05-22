using UnityEngine;
using UnityEngine.UI;

public class PlayerDataPlace : MonoBehaviour
{
    public static PlayerDataPlace Instance;

    [Header("Player Info")]
    public string playerName = "";

    [Header("Currency")]
    public int coinCount = 85;

    [Header("Session Data")]
    public float playTime = 0f;

    [Header("Levels Complete")]
    public int Levels = 0;

    [Header("Character Selected")]
    public bool Female=false;
    public bool Male=false;

    [Header("StarerMenuSettings")]
    public bool StarerMenuDoNotRepeat = true;

    [Header("Others-Level3")]
    public bool Level3IntActive = true;

    public bool Level3CoinsInactive = true;
    public bool Level2CoinsInactive = true;
    public bool Level1CoinsInactive = true;
    public bool canInteract=false;
    public string InputNameValue = "";


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        playTime += Time.deltaTime;
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void RemoveCoins(int amount)
    {
        coinCount = Mathf.Max(0, coinCount - amount);
    }

    public void ResetPlayTime()
    {
        playTime = 0f;
    }

    public void CharacterSelectionMale()
    {
        Male=false;
    }

    public void CharacterSelectionFemale()
    {
        Female=false;
    }

    public void StarterMenuMethod()
    {
        StarerMenuDoNotRepeat = false;
    }
}
