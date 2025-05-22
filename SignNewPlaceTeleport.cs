using UnityEngine;

public class SignNewPlaceTeleport : MonoBehaviour
{
    public GameObject Arrow;
    public PointerScript ManagerLocationArrows;
    public int movingClearance = 1;
    private bool hasExited = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasExited)
        {
            if (movingClearance == 1)
            {
                ManagerLocationArrows.TeleportPlace1();
                movingClearance++;
            }
            else if (movingClearance == 2)
            {
                ManagerLocationArrows.TeleportPlace2();
                movingClearance++;
            }
            else if (movingClearance == 3)
            {
                ManagerLocationArrows.TeleportPlace3();
                movingClearance++;
            }
            else if (movingClearance == 4)
            {
                ManagerLocationArrows.TeleportPlace4();
                movingClearance++;
            }
            else if (movingClearance == 5)
            {
                ManagerLocationArrows.FinalArrowDelete();
                movingClearance++;
            }

            hasExited = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasExited = true;
        }
    }
}
