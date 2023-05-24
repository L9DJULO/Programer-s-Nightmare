using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBackInTime : MonoBehaviour
{
    public static bool HaveBackInTime = false;
    public GameObject Panel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(_enumerator(other));
        }
    }
    IEnumerator _enumerator(Collider other)
    {
        Panel.SetActive(true);
        yield return new WaitForSeconds(3);
        Panel.SetActive(false);
        Pickup(other);
    }
    void Pickup(Collider player)
    {
        // Apply effect to the player
        HaveBackInTime = true;
        // Remove PowerUp 
        Destroy(gameObject);
    }
    
}
