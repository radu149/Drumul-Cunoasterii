using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinsFinalCarLevel : MonoBehaviour
{
    public TextMeshProUGUI CoinsReward;

    public PlayerDataPlace PlayerDataPlace;

    void Start()
    {
        CoinsReward.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CoinsReward.text = "Ai castigat 10 bani!";
            PlayerDataPlace.Instance.coinCount += 10;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
