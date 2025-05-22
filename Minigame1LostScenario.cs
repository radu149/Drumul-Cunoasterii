using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Minigame1LostScenario : MonoBehaviour
{
//    public GameObject DieCollider1;
//    public GameObject DieCollider2;

    public Button respawnButton;
    public TextMeshProUGUI DeathMessage;
    public Vector3 initialPositionPlayerMG1 = new Vector3(-0.11f, 0f, -6.86f); 
    public GameObject PlayerMale;
    public GameObject PlayerFemale;
    public Image BackroundDeathText;

    public PlayerDataPlace PlayerDataPlace;
    public ThirdPersonController thirdPersonController;
    public CarSystem CarManager;

    void Start()
    {
        respawnButton.gameObject.SetActive(false);
        DeathMessage.text="";
        respawnButton.onClick.AddListener(RespawnButtonSys);
        BackroundDeathText.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if ((other.gameObject == DieCollider1 || other.gameObject == DieCollider2) && other.CompareTag("Player"))
//        {
//            Death();
//        }
//    }
    
    public void Death()
    {
        respawnButton.gameObject.SetActive(true);
        BackroundDeathText.gameObject.SetActive(true);
        DeathMessage.text="Ai pierdut!";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        thirdPersonController.OptionsMovementNull = false;
        CarManager.CarWorking = false;
    }

    void RespawnButtonSys()
    {
        respawnButton.gameObject.SetActive(false);
        DeathMessage.text="";
    //    if(PlayerDataPlace.Instance.Male)
    //    {
            PlayerMale.transform.position = initialPositionPlayerMG1;
    //    }

    //    if(PlayerDataPlace.Instance.Female)
    //    {
    //        PlayerFemale.transform.position = initialPositionPlayerMG1;
    //    }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        thirdPersonController.OptionsMovementNull = true;
        CarManager.CarWorking = true;
        BackroundDeathText.gameObject.SetActive(false);
    }
}
