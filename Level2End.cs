using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level2End : MonoBehaviour
{
    public TextMeshProUGUI textEndLevel;
    public PlayerDataPlace PlayerDataPlace;
    void Start()
    {
        textEndLevel.text = "";
        textEndLevel.gameObject.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            textEndLevel.gameObject.SetActive(true);
            textEndLevel.text = "Ai castigat 15 monede!";
            if (PlayerDataPlace.Instance.Level1CoinsInactive)
            {
                PlayerDataPlace.Instance.coinCount += 35;
                PlayerDataPlace.Instance.Levels += 1;
            }
            PlayerDataPlace.Instance.Level1CoinsInactive = false;
            Invoke(nameof(FinalLevelRewards), 3f);
        }
    }
    void FinalLevelRewards()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
