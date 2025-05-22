using UnityEngine;

public class DeathRobot : MonoBehaviour
{
    public Vector3 spawnPoint = new Vector3(0.2f, 0.535f, -2.5f);
    public float AdaosYLever = 10f;
    public GameObject robot;
    public GameObject playerM;
    public GameObject playerF;
    void Start()
    {
        playerM.transform.position = new Vector3(0.2f, AdaosYLever, -2.5f);
        playerF.transform.position = new Vector3(0.2f, AdaosYLever, -2.5f);

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Robot"))
        {
            robot.transform.position = spawnPoint;
            Debug.Log("Robot has been reset to spawn point");
        }
    }
}
