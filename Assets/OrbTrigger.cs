using UnityEngine;

public class OrbTrigger : MonoBehaviour
{
    public CountdownTimer countdownTimer; // Référence au script CountdownTimer attaché à un autre GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countdownTimer.StartTimer(); // Démarre le timer lorsque l'orbe entre en contact avec le joueur
            Destroy(gameObject); // Détruit l'orbe après l'avoir touchée
        }
    }
}
