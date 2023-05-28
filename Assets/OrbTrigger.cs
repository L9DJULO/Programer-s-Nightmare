using UnityEngine;

public class OrbTrigger : MonoBehaviour
{
    public CountdownTimer countdownTimer; // R�f�rence au script CountdownTimer attach� � un autre GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countdownTimer.StartTimer(); // D�marre le timer lorsque l'orbe entre en contact avec le joueur
            Destroy(gameObject); // D�truit l'orbe apr�s l'avoir touch�e
        }
    }
}
