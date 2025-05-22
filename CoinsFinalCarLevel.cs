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
        CoinsReward.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinsReward.gameObject.SetActive(true);
            CoinsReward.text = "Ai castigat 15 monede!";
            if (PlayerDataPlace.Instance.Level2CoinsInactive)
            {
                PlayerDataPlace.Instance.coinCount += 15;
                PlayerDataPlace.Instance.Levels += 1;
            }
            PlayerDataPlace.Instance.Level2CoinsInactive = false;
            Invoke(nameof(FinalLevel2), 3f);
        }
    }

    void FinalLevel2()
    { 
        SceneManager.LoadScene("SampleScene");
    }
}
