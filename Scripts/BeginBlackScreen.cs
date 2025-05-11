using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeginBlackScreen : MonoBehaviour
{
    public Image blackscreen;
    public float TimerDuration=3f;

    void Start()
    {
        blackscreen.gameObject.SetActive(true);

        StartCoroutine(TimerD());
    }

    private IEnumerator TimerD()
    {
        float timeLeft = TimerDuration;

        while (timeLeft > 0)
        {
            Debug.Log("TimerDebug");
            yield return null;
            timeLeft -= Time.deltaTime;
        }

        blackscreen.gameObject.SetActive(false);

        Debug.Log("Starter Black Scrren is gone");
    }
}
