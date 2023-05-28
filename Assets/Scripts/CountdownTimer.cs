using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float timeValue = 60f; // Durée du compte à rebours en secondes
    public Text timerText; // Référence à l'élément de texte pour afficher le timer
    private bool isRunning = false;

    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false); // Désactive l'élément de texte au départ
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
                StopTimer();
            }

            DisplayTime(timeValue);
        }
    }

    public void StartTimer()
    {
        isRunning = true;
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true); // Active l'élément de texte lorsque le timer démarre
            timerText.text = "Temps Restant : " + Mathf.FloorToInt(timeValue).ToString();
        }
    }

    private void StopTimer()
    {
        isRunning = false;
        timerText.gameObject.SetActive(false); // Désactive l'élément de texte lorsque le timer s'arrête
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        if (timerText != null)
        {
            timerText.text = "Temps Restant : " + Mathf.FloorToInt(timeToDisplay).ToString();
        }
    }
}