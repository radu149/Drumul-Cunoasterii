using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Minigame1Starter : MonoBehaviour
{
    public PlayerDataPlace PlayerDataPlace;

    public GameObject MalePlayer;
    public GameObject FemalePlayer;

    public Camera CameraM;
    public Camera CameraF;

    void Start()
    {
        if(PlayerDataPlace.Instance.Male)
        {
            MalePlayer.gameObject.SetActive(true);
            FemalePlayer.gameObject.SetActive(false);
            CameraM.gameObject.SetActive(true);
        }
        else if(PlayerDataPlace.Instance.Female)
        {
            FemalePlayer.gameObject.SetActive(true);
            MalePlayer.gameObject.SetActive(false);
            CameraF.gameObject.SetActive(true);
        }
    }
}
