using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public CountdownTimer countdownTimer; // Référence au script CountdownTimer attaché à un autre GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countdownTimer.StartTimer(); // Démarre le timer lorsque le joueur touche le sol
            Destroy(gameObject); // Détruit le collider du sol pour éviter de déclencher le timer à nouveau
        }
    }
}