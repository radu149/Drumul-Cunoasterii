using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinancialGameManager : MonoBehaviour
{
    [Header("Item References")]
    public GameObject banknoteObject;
    public GameObject appleObject;
    public GameObject phoneObject;

    [Header("UI")]
    public Button buyButton;
    public TMP_Text messageText;

    [Header("Costs")]
    public int banknoteCost = 10;
    public int banknoteReturn = 30;
    public int appleCost = 20;
    public int phoneCost = 100;

    [Header("Typewriter Effect")]
    public float typeSpeed = 0.05f;
    public string[] successMessages;

    private GameObject selectedItem;
    private string selectedName = "";
    private bool hasBoughtBanknote = false;
    private bool hasBoughtApple = false;
    private bool hasBoughtPhone = false;

    private PlayerDataPlace playerData;

    private void Start()
    {
        playerData = PlayerDataPlace.Instance;
        buyButton.onClick.AddListener(BuySelectedItem);
        messageText.text = "";
    }

    private void Update()
    {
        HandleKeySelection();
    }

    private void HandleKeySelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItem = appleObject;
            selectedName = "Marul (necesitate)";
            messageText.text = "Ai selectat: Marul";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedItem = phoneObject;
            selectedName = "Telefonul (lux)";
            messageText.text = "Ai selectat: Telefonul";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedItem = banknoteObject;
            selectedName = "Bacnota (investitie)";
            messageText.text = "Ai selectat: Bacnota";
        }
    }

    private void BuySelectedItem()
    {
        if (selectedItem == null || playerData == null)
        {
            messageText.text = "Nu ai selectat nimic!";
            return;
        }

        if (selectedItem == banknoteObject && !hasBoughtBanknote && playerData.coinCount >= banknoteCost)
        {
            playerData.RemoveCoins(banknoteCost);
            playerData.AddCoins(banknoteReturn);
            hasBoughtBanknote = true;
            messageText.text = "Ai investit 10 monede și ai primit 30! Bravo!";
        }
        else if (selectedItem == appleObject && hasBoughtBanknote && !hasBoughtApple && playerData.coinCount >= appleCost)
        {
            playerData.RemoveCoins(appleCost);
            hasBoughtApple = true;
            messageText.text = "Ai cumparat marul (necesitate).";
        }
        else if (selectedItem == phoneObject && hasBoughtBanknote && !hasBoughtPhone && playerData.coinCount >= phoneCost)
        {
            playerData.RemoveCoins(phoneCost);
            hasBoughtPhone = true;
            messageText.text = "Ai cumparat telefonul (lux).";
        }
        else
        {
            messageText.text = "Nu poti cumpara acest obiect inca sau nu ai bani!";
        }

        if (hasBoughtBanknote && hasBoughtApple && hasBoughtPhone)
        {
            StartCoroutine(ShowMessagesAndReturn());
        }
    }

    private IEnumerator ShowMessagesAndReturn()
    {
        buyButton.interactable = false;

        foreach (string msg in successMessages)
        {
            yield return StartCoroutine(TypeWriter(msg));
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator TypeWriter(string message)
    {
        messageText.text = "";
        foreach (char c in message)
        {
            messageText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
