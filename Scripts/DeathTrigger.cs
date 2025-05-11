using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public Minigame1LostScenario manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.Death();
        }
    }
}
