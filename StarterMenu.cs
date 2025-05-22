using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarterMenu : MonoBehaviour
{
    [Header("TextElements")]
    public TextMeshProUGUI Text;
    public string [] messages;
    public float countdownTextTimer=3f;

    [Header("BlackScreenStarter")]
    public Image blackscreen;
//  public float TimerDuration=3f;
    public TextMeshProUGUI CapitationText;

    [Header("CodeRunningEssentials")]
    private bool SequenceLevel2=false;
    private bool SequenceLevel1=false;
    public bool StartMenuButtonPressed;
    public Camera CameraMenu;
    public bool canInteract=false;
    public float interactRange = 5f;
    private float timeSwitch=1.5f;
    private bool MaleVer=false;
    private bool FemaleVer=false;
    private bool QuizEnded=false;
    public PlayerDataPlace PlayerDataPlace;
    public OptionsScript optionsScript;

    [Header("MessageIndications")]
    public TextMeshProUGUI Message1;

    [Header("Characters")]
    public GameObject Male;
    public GameObject Female;
//  public Button MaleButton;
//  public Button FemaleButton;
    public GameObject ButtonBlue;
    public GameObject ButtonRed;
    public GameObject MaleController;
    public GameObject FemaleController;
    public Camera CameraMale;
    public Camera CameraFemale;
//  public TextMeshProUGUI SelectableText;


    public float maxDistance = 100f;
//    public LayerMask interactableLayer; 
    private GameObject currentTarget;

    [Header("QuizElements")]
//    public GameObject question1;
    public InputField answer1;
    public TextMeshProUGUI feedbackText;
    public Button submitButton;

//    [Header("Buttons")]
//    public Button StartGameButton;
//    public Button ExitGame;



    void Start()
    {   
//        StartGameButton.onClick.AddListener(OnButtonPressedS1);
//        ExitGame.onClick.AddListener(OnButtonPressedS2);
        if(PlayerDataPlace.Instance.Male)
        {
            blackscreen.gameObject.SetActive(false);
            MaleController.gameObject.SetActive(true);
            FemaleController.gameObject.SetActive(false);
            CameraMale.gameObject.SetActive(true);
        }
        else if(PlayerDataPlace.Instance.Female)
        {
            blackscreen.gameObject.SetActive(false);
            FemaleController.gameObject.SetActive(true);
            MaleController.gameObject.SetActive(false);
            CameraFemale.gameObject.SetActive(true);
        }
        
        if (PlayerDataPlace.Instance.StarerMenuDoNotRepeat)
        {
            CameraMenu.gameObject.SetActive(true);
            CapitationText.gameObject.SetActive(true);
            StartCoroutine(DisplayMessages());
            Cursor.lockState = CursorLockMode.None;

            blackscreen.gameObject.SetActive(true);
            //       StartCoroutine(TimerD());

            Male.gameObject.SetActive(true);
            Female.gameObject.SetActive(true);
            Message1.gameObject.SetActive(false);
            Message1.text = "";

            QuizInvis();
        }
    }

    void Update()
    {
        if(SequenceLevel1 == true)
        {
            Message1.text="Ce caracter doresti sa utilizezi?";
            MaleFemaleSelection();
            Ray ray = new Ray(CameraMenu.transform.position, CameraMenu.transform.forward);
            RaycastHit hit;

             if (Physics.Raycast(ray, out hit, maxDistance))
        {
            currentTarget = hit.collider.gameObject;

            Debug.DrawLine(ray.origin, hit.point, Color.green);

            if (Input.GetKeyDown(KeyCode.M))
            {
                DoSomethingA();
                Invoke("MaleFemaleOut", timeSwitch);
                Debug.Log("M pressed on: " + currentTarget.name);
                SequenceLevel1=false;
                SequenceLevel2=true;
//                currentTarget.SendMessage("OnMKeyPressed", SendMessageOptions.DontRequireReceiver);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                DoSomethingB();
                Invoke("MaleFemaleOut", timeSwitch);
                Debug.Log("F pressed on: " + currentTarget.name);
                SequenceLevel1=false;
                SequenceLevel2=true;
//                currentTarget.SendMessage("OnFKeyPressed", SendMessageOptions.DontRequireReceiver);
            }
        }
//        else
//        {
//            currentTarget = null;
//        }

//            if (Input.GetKeyDown("m") && GetComponent<Collider>().CompareTag("ButtonMale"))
//            {
//                DoSomethingA();
//                Invoke("MaleFemaleOut", timeSwitch);
//               Debug.Log("Male");
//            }
//            if (Input.GetKeyDown("f") && GetComponent<Collider>().CompareTag("ButtonFemale"))
//            {
//                DoSomethingB();
//                Invoke("MaleFemaleOut", timeSwitch);
//                Debug.Log("Female");
//            }
        }

        if (SequenceLevel2)
        {
            Quiz();
            submitButton.onClick.AddListener(OnSubmit);
            //            Debug.Log("Sequence2 ran succesfully");
            PlayerDataPlace.Instance.StarterMenuMethod();
        }
    }

    private IEnumerator DisplayMessages()
    {
//        Text.gameObject.SetActive(true);
        foreach (string message in messages)
        {
            Text.text = message;

            yield return StartCoroutine(FadeText(0f, 1f, 1f));

            yield return new WaitForSeconds(countdownTextTimer);

            yield return StartCoroutine (FadeText(1f, 0f, 1f));
        }

        Text.text="";
        blackscreen.gameObject.SetActive(false);
//        Text.gameObject.SetActive(false);        
        SequenceLevel1=true;
        Debug.Log("All messages displayed!");
    }
    
    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        Color color = Text.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
        elapsed += Time.deltaTime;
        color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
        Text.color = color;
        yield return null;
        }

        color.a = endAlpha;
        Text.color = color;
        
    }

 //   private IEnumerator TimerD()
//    {
//        float timeLeft = TimerDuration;
//
//        while (timeLeft > 0)
//        {
//            Debug.Log("TimerDebug");
//            yield return null;
//            timeLeft -= Time.deltaTime;
//        }
//
//        blackscreen.gameObject.SetActive(false);
//
//        Debug.Log("Starter Black Scrren is gone");
//    }

    public void MaleFemaleSelection()
    {
        Male.gameObject.SetActive(true);
        Female.gameObject.SetActive(true);
//        MaleButton.gameObject.SetActive(true);
//        FemaleButton.gameObject.SetActive(true);
        Message1.gameObject.SetActive(true);
        ButtonBlue.gameObject.SetActive(true);
        ButtonRed.gameObject.SetActive(true);
        Message1.gameObject.SetActive(true);
    }

    public void MaleFemaleOut()
    {
        Male.gameObject.SetActive(false);
        Female.gameObject.SetActive(false);
//        MaleButton.gameObject.SetActive(false);
//        FemaleButton.gameObject.SetActive(false);
        Message1.gameObject.SetActive(false);
        ButtonBlue.gameObject.SetActive(false);
        ButtonRed.gameObject.SetActive(false);
        Message1.gameObject.SetActive(false);
        Message1.text="";
        SequenceLevel1=false;
    }

    void DoSomethingA()
    {
        MaleVer=true;
        MaleFemaleOut();
    }

    void DoSomethingB()
    {
        MaleVer=true;
        MaleFemaleOut();
    }

    void Quiz()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
//        question1.SetActive(true);
        submitButton.gameObject.SetActive(true);
        answer1.gameObject.SetActive(true);
        feedbackText.text = "Introduce un nume";
//        Debug.Log(PlayerData.playerName);

        if(QuizEnded)
        {
            if(MaleVer)
            {
                CameraMenu.gameObject.SetActive(false);
                MaleController.gameObject.SetActive(true);
                CameraMale.gameObject.SetActive(true);
//                PlayerDataPlace sn = gameObject.GetComponent<PlayerDataPlace>();
//                sn.CharacterSelectionMale();
                QuizInvis();
                PlayerDataPlace.Instance.Male = true;
//                canInteract=true;
                SequenceLevel2=false;
                
            }

            else if(FemaleVer)
            {
                CameraMenu.gameObject.SetActive(false);
                FemaleController.gameObject.SetActive(true);
                CameraFemale.gameObject.SetActive(true);
//                PlayerDataPlace sn = gameObject.GetComponent<PlayerDataPlace>();
//                sn.CharacterSelectionFemale();
                
                QuizInvis();
                PlayerDataPlace.Instance.Female = true;
//                canInteract=true;
                SequenceLevel2=false;
            }
        }
    }

    void QuizInvis()
    {
        feedbackText.text="";
        submitButton.gameObject.SetActive(false);
        answer1.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnSubmit()
    {
        if(SequenceLevel2)
        {
            string playerName = answer1.text;

            if (!string.IsNullOrWhiteSpace(playerName))
            {
//                PlayerData.playerName = answer1.text;
//                Debug.Log("Player name set to: " + PlayerDataPlace.Instance.playerName);
                PlayerDataPlace.Instance.SetPlayerName(answer1.text);
                PlayerDataPlace.Instance.InputNameValue = playerName;
                PlayerDataPlace.Instance.canInteract=true;
                QuizEnded=true;
            }
        }
    }

    public void CharacterSelectionMale()
    {
        
    }
}
