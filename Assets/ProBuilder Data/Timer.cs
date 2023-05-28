using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeValue = 10f;
    private bool isRunning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRunning)
        {
            isRunning = true;
            StartTimer();
        }
    }

    private void StartTimer()
    {
        InvokeRepeating("Countdown", 0f, 1f);
    }

    private void Countdown()
    {
        if (timeValue > 0)
        {
            Debug.Log("Temps restant : " + timeValue);
            timeValue -= 1f;
        }
        else
        {
            StopTimer();
            Debug.Log("Le temps est écoulé !");
        }
    }

    private void StopTimer()
    {
        isRunning = false;
        CancelInvoke("Countdown");
    }
}