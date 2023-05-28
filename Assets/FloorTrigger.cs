using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public CountdownTimer countdownTimer; // R�f�rence au script CountdownTimer attach� � un autre GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countdownTimer.StartTimer(); // D�marre le timer lorsque le joueur touche le sol
            Destroy(gameObject); // D�truit le collider du sol pour �viter de d�clencher le timer � nouveau
        }
    }
}