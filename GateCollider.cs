using UnityEngine;

public class GateCollider : MonoBehaviour
{

    public GameObject gate;
    void Start()
    {
        gate.SetActive(true);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            Debug.Log("Robot has passed the gate");
            gate.SetActive(false);
        }
        else if(other.CompareTag("Player"))
        {
            Debug.Log("Player has passed the gate");
            gate.SetActive(true);
        }
    }   
}
