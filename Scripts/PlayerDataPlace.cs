using UnityEngine;

public class PlayerDataPlace : MonoBehaviour
{
    public static PlayerDataPlace Instance;

    [Header("Player Info")]
    public string playerName = "";

    [Header("Currency")]
    public int coinCount = 0;

    [Header("Session Data")]
    public float playTime = 0f;

    [Header("Levels Complete")]
    public int Levels = 0;

    [Header("Character Selected")]
    public bool Female=false;
    public bool Male=false;


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

}
