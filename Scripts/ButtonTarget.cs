using UnityEngine;

public class ButtonTarget : MonoBehaviour
{
    public void OnMKeyPressed()
    {
        Debug.Log("M triggered on " + name);
        // Custom M behavior here
    }

    public void OnFKeyPressed()
    {
        Debug.Log("F triggered on " + name);
        // Custom F behavior here
    }
}
