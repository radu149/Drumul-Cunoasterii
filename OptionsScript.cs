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

    [Header("PostProcessing")]
    public BrightnessController brightnessController;

    [Header("Player Data")]
//    public InputField nameInputField;
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
    public Button Settings;
    public Button backButton2;

    [Header("Stats UI")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelsText;
    public Button backButton1;

    [Header("Sliders")]
    public Slider _sliderSensitivity;
    public TextMeshProUGUI _sliderText1;

    public Slider _sliderBrightness;
    public TextMeshProUGUI _sliderText2; 

    public TextMeshProUGUI TextSensitivity;
    public TextMeshProUGUI TextBrightness;

    private void Start()
    {
        if (PlayerDataPlace.Instance != null)
        {
            playerDataPlace = PlayerDataPlace.Instance;
//            playerDataPlace.SetPlayerName(nameInputField.text);

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

//    void Awake()
//{
//    if (instance != null && instance != this)
//    {
//        Destroy(gameObject); 
//        return;
//    }

//    instance = this;
//    DontDestroyOnLoad(gameObject);
//}

    private void Update()
    {
        if (PlayerDataPlace.Instance.canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuOpen = !menuOpen;
                ToggleMenu(menuOpen);
                thirdPersonController.OptionsMovementNull = !menuOpen;
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
        statsButton.onClick.AddListener(OnButtonPressed4);
        quitGameButton.onClick.AddListener(OnButtonPressed5);
        quitScenarioButton.onClick.AddListener(OnButtonPressed6);
        backButton1.onClick.AddListener(OnButtonPressed7);
//        Settings.onClick.AddListener(OnButtonPressed8);
//        backButton2.onClick.AddListener(OnButtonPressed9);
//        _sliderSensitivity.onValueChanged.AddListener((v1) =>{
//            _sliderText1.text = v1.ToString("0");
//            thirdPersonController.thirdPersonCamera.SetCameraSensitivity(v1);
//        });
//        _sliderBrightness.onValueChanged.AddListener((v2) =>{
//            _sliderText2.text = v2.ToString("0");
//            brightnessController.SetBrightness(v2);
//        });
    }

    private void ToggleMenu(bool show)
    {
        menuOpen = show;
        if (statsButton != null)
            statsButton.gameObject.SetActive(show);

        if (quitGameButton != null)
            quitGameButton.gameObject.SetActive(show);

        if (quitScenarioButton != null)
            quitScenarioButton.gameObject.SetActive(show);
//        Settings.gameObject.SetActive(show);

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
        Debug.Log("Save data cleared.");
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
        backButton1.gameObject.SetActive(true);

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
        backButton1.gameObject.SetActive(false);

        ToggleMenu(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        thirdPersonController.OptionsMovementNull=false;
    }
    private void UpdateStatsUI()
    {
        if (playerDataPlace == null) return;

        if (coinsText != null)
            coinsText.text = $"Monede: {playerDataPlace.coinCount}";

        if (nameText != null)
            nameText.text = $"Numele Jucatorului: {playerDataPlace.playerName}";

        if (timeText != null)
                timeText.text = $"Timp Jucat: {minutes:00}:{seconds:00}";

        if (levelsText != null)
            levelsText.text = $"Nivele Incheiate: {playerDataPlace.Levels}";
    }

    private void OnButtonPressed1()
    {
        Save();
        Debug.Log("Button was pressed and is visible!");
    }

    private void OnButtonPressed2()
    {
        LoadGame();
        Debug.Log("Button was pressed and is visible!");
    }

    private void OnButtonPressed3()
    {
        DeleteSave();
        Debug.Log("Button was pressed and is visible!");
    }

    private void OnButtonPressed4()
    {
        ShowStats();
        Debug.Log("Button was pressed and is visible!");
    }

    private void OnButtonPressed5()
    {
        QuitGame();
        Debug.Log("Button was pressed and is visible!");
    }

    private void OnButtonPressed6()
    {
        QuitScenario();
        Debug.Log("Button was pressed and is visible!");
    }

    private void OnButtonPressed7()
    {
        HideStats();
        Debug.Log("Button was pressed and is visible!");
    }

//    private void OnButtonPressed8()
//    {
//        SliderToggleMenuOn();
//        ToggleMenu(false);
//        Debug.Log("Settings button was clicked!");
//    }

//    private void OnButtonPressed9()
//    {
//        SliderToggleMenuOff();
//        ToggleMenu(true);
//    }

//    private void SliderToggleMenuOn()
//    {
//        _sliderSensitivity.gameObject.SetActive(true);
//        _sliderBrightness.gameObject.SetActive(true);
//        _sliderText1.gameObject.SetActive(true);
//        _sliderText2.gameObject.SetActive(true);
//        TextSensitivity.gameObject.SetActive(true);
//        TextBrightness.gameObject.SetActive(true);
//        backButton2.gameObject.SetActive(true);

//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;

//        thirdPersonController.OptionsMovementNull=false;
//    }

       // private void SliderToggleMenuOff()
//    {
//        _sliderSensitivity.gameObject.SetActive(false);
//        _sliderBrightness.gameObject.SetActive(false);
//        _sliderText1.gameObject.SetActive(false);
//        _sliderText2.gameObject.SetActive(false);
//        TextSensitivity.gameObject.SetActive(false);
//        TextBrightness.gameObject.SetActive(false);
//        backButton2.gameObject.SetActive(false);
//    }
}
