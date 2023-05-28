using UnityEngine;


public class CountdownTimer : MonoBehaviour
{
    public float timeValue = 60f;
    private bool isRunning = false;

    public GameObject levelCompleteMessage; // Référence au GameObject contenant le message de réussite du niveau

    private void Start()
    {
        levelCompleteMessage.SetActive(false);
        StartTimer();// Assurez-vous que le message de réussite du niveau est désactivé au début
    }

    public void StartTimer()
    {
        isRunning = true;
        InvokeRepeating("Countdown", 0f, 1f);
    }

    private void Countdown()
    {
        if (timeValue > 0)
        {
            Debug.Log("Time Remaining: " + timeValue);
            timeValue -= 1f;
        }
        else
        {
            StopTimer();
            Debug.Log("Time's Up!");
        }
    }

    private void StopTimer()
    {
        isRunning = false;
        CancelInvoke("Countdown");
        levelCompleteMessage.SetActive(true); // Active le message de réussite du niveau
    }
}