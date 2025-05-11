using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    [Header("Core References")]
    public StarterMenu starterMenu;
    public PlayerDataPlace playerDataPlace;
    public ThirdPersonController thirdPersonController;
    public Transform playerTransform;
    public Image menuScreen;

    [Header("Player Data")]
    public InputField nameInputField;
    private string playerName;
    private int coinCount;
    private float playTime;
    private int levelsCompleted;
    private int minutes;
    private int seconds;

    private bool menuOpen = false;

    private static OptionsScript instance;

    [Header("Options Menu Buttons")]
    public Button saveButton;
    public Button loadButton;
    public Button deleteSaveButton;
    public Button statsButton;
    public Button quitGameButton;
    public Button quitScenarioButton;

    [Header("Stats UI")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelsText;
    public Button backButton;

    private void Start()
    {
        if (PlayerDataPlace.Instance != null)
        {
            playerDataPlace = PlayerDataPlace.Instance;
            playerDataPlace.SetPlayerName(nameInputField.text);

            playerName = playerDataPlace.playerName;
            coinCount = playerDataPlace.coinCount;
            playTime = playerDataPlace.playTime;
            levelsCompleted = playerDataPlace.Levels;
        }
        else
        {
            Debug.LogWarning("PlayerDataPlace.Instance is null!");
        }

        AssignButtonListeners();
    }

    void Awake()
{
    if (instance != null && instance != this)
    {
        Destroy(gameObject); 
        return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);
}

    private void Update()
    {
        if (starterMenu.canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleMenu(true);
                thirdPersonController.OptionsMovementNull=false;
            }
            else if (menuOpen && Input.GetKeyDown(KeyCode.Q))
            {
                ToggleMenu(false);
                thirdPersonController.OptionsMovementNull=true;
            }

            if (playerDataPlace != null)
            {
                minutes = Mathf.FloorToInt(playerDataPlace.playTime / 60f);
                seconds = Mathf.FloorToInt(playerDataPlace.playTime % 60f);
            }

            UpdateStatsUI();
        }
    }

    private void AssignButtonListeners()
    {

        saveButton.onClick.AddListener(OnButtonPressed1);
        loadButton.onClick.AddListener(OnButtonPressed2);
        deleteSaveButton.onClick.AddListener(OnButtonPressed3);
        statsButton.onClick.AddListener(OnButtonPressed4);
        quitGameButton.onClick.AddListener(OnButtonPressed5);
        quitScenarioButton.onClick.AddListener(OnButtonPressed6);
        backButton.onClick.AddListener(OnButtonPressed7);
    }

    private void ToggleMenu(bool show)
    {
        menuOpen = show;

//        saveButton.gameObject.SetActive(show);
//        loadButton.gameObject.SetActive(show);
//        deleteSaveButton.gameObject.SetActive(show);
        statsButton.gameObject.SetActive(show);
        quitGameButton.gameObject.SetActive(show);
        quitScenarioButton.gameObject.SetActive(show);
//        menuScreen.gameObject.SetActive(show);

        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = show;

        thirdPersonController.OptionsMovementNull = !show;
    }

    private void Save()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetFloat("PlayerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerTransform.position.z);
        PlayerPrefs.SetInt("CoinsCountNr", coinCount);
        PlayerPrefs.SetInt("LevelsCompletedNr", levelsCompleted);
        PlayerPrefs.Save();

        Debug.Log("Game Saved!");
    }

    private void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName = PlayerPrefs.GetString("PlayerName");

            Vector3 pos = new Vector3(
                PlayerPrefs.GetFloat("PlayerPosX"),
                PlayerPrefs.GetFloat("PlayerPosY"),
                PlayerPrefs.GetFloat("PlayerPosZ")
            );
            playerTransform.position = pos;

            coinCount = PlayerPrefs.GetInt("CoinsCountNr");
            levelsCompleted = PlayerPrefs.GetInt("LevelsCompletedNr");

            Debug.Log($"Game Loaded! Player: {playerName}, Coins: {coinCount}");
        }
        else
        {
            Debug.LogWarning("No saved game found.");
        }
    }

    private void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void QuitScenario()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void ShowStats()
    {
        ToggleMenu(false);

        nameText.gameObject.SetActive(true);
        coinsText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        levelsText.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        thirdPersonController.OptionsMovementNull=false;
    }

    private void HideStats()
    {
        nameText.gameObject.SetActive(false);
        coinsText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        levelsText.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);

        ToggleMenu(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        thirdPersonController.OptionsMovementNull=false;
    }

    private void UpdateStatsUI()
    {
        if (playerDataPlace == null) return;

        coinsText.text = $"Monede: {playerDataPlace.coinCount}";
        nameText.text = $"Numele Jucatorului: {playerDataPlace.playerName}";
        timeText.text = $"Timp Jucat: {minutes:00}:{seconds:00}";
        levelsText.text = $"Nivele Incheiate: {playerDataPlace.Levels}";
    }

    private void OnButtonPressed1()
    {
        Save();
    }

        private void OnButtonPressed2()
    {
        LoadGame();
    }

        private void OnButtonPressed3()
    {
        DeleteSave();
    }

        private void OnButtonPressed4()
    {
        ShowStats();
    }

        private void OnButtonPressed5()
    {
        QuitGame();
    }

        private void OnButtonPressed6()
    {
        QuitScenario();
    }

        private void OnButtonPressed7()
    {
        HideStats();
    }

}
