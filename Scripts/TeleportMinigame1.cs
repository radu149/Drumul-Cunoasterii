using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMinigame1 : MonoBehaviour
{
    public GameObject Portal;

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            SceneManager.LoadScene("Minigame1");
        }
    }
}
