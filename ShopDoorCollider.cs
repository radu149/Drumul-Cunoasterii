using UnityEngine;

public class ShopDoorCollider : MonoBehaviour
{
    public GameObject door;
    public ShopLevelManager ShopLevelManager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShopLevelManager.CameraSystemChanging();
        }
    }
}
