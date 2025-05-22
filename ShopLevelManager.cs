using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopLevelManager : MonoBehaviour
{
    public Camera CameraMaler;
    public Camera CameraShop;
    public Camera CameraFemaler;
    public bool Target1bool = true;
    public bool Target2bool = false;
    public PlayerDataPlace PlayerDataPlace;
    public Button button1;
    public TextMeshProUGUI text1;
    void Start()
    {
        CameraShop.enabled = false;
    }
    
    public void CameraSystemChanging()
    {
        if (Target1bool)
        {
            if (PlayerDataPlace.Instance.Male)
            {
                CameraMaler.enabled = false;
            }
            else if (PlayerDataPlace.Instance.Female)
            {
                CameraFemaler.enabled = false;
            }

            CameraShop.enabled = true;
            text1.gameObject.SetActive(true);
            button1.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
//            Target1bool = false;
            //            Target2bool = true;
            //            CameraShop.enabled = true;
        }
    }
}
