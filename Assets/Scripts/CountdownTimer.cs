using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float timeValue = 60f; // Dur�e du compte � rebours en secondes
    public Text timerText; // R�f�rence � l'�l�ment de texte pour afficher le timer
    private bool isRunning = false;

    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false); // D�sactive l'�l�ment de texte au d�part
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
            timerText.gameObject.SetActive(true); // Active l'�l�ment de texte lorsque le timer d�marre
            timerText.text = "Temps Restant : " + Mathf.FloorToInt(timeValue).ToString();
        }
    }

    private void StopTimer()
    {
        isRunning = false;
        timerText.gameObject.SetActive(false); // D�sactive l'�l�ment de texte lorsque le timer s'arr�te
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