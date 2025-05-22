using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class IntroductionLevel3 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;
    public TextMeshProUGUI SpaceTextPress;
    public TextMeshProUGUI ObjectiveText;

    public string[] messages; 
    public GameObject imageToHide;  

    private int currentMessageIndex = 0;
    private bool isTyping = false;
    private bool messageFinished = false;
    public bool IntroductionDone3 = false;

    public PlayerDataPlace playerDataPlace;
    public ElocutionChat elocutionChat;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (messages.Length > 0 && PlayerDataPlace.Instance.Level3IntActive)
        {
            StartCoroutine(TypeMessage(messages[currentMessageIndex]));
        }
        imageToHide.SetActive(true);
//        SpaceTextPress.gameObject.SetActive(true);
    }

    void Update()
    {
        if (messageFinished && Input.GetKeyDown(KeyCode.Space))
        {
            SpaceTextPress.gameObject.SetActive(false);
            currentMessageIndex++;

            if (currentMessageIndex < messages.Length)
            {
                messageFinished = false;
                StartCoroutine(TypeMessage(messages[currentMessageIndex]));
            }
            else
            {
                dialogueText.text = "";
                if (imageToHide != null)
                {
                    imageToHide.SetActive(false);
                }
                ObjectiveText.gameObject.SetActive(true);
                SpaceTextPress.gameObject.SetActive(false);
                dialogueText.gameObject.SetActive(false);
                IntroductionDone3 = true;
                elocutionChat.ActiveMechanicPanel.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        messageFinished = true;

        if(messageFinished)
        {
        SpaceTextPress.gameObject.SetActive(true);
        }
    }
}