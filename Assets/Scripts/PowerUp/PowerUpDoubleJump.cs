using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDoubleJump : MonoBehaviour
{
    public GameObject Panel;
    public GameObject pickupEffect;
    public static bool canDoubleJump = false;
    void Start()
    {
        
    }
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
        yield return new WaitForSeconds(2);
        Panel.SetActive(false);
        Pickup(other);
    }
    void Pickup(Collider player)
    {
        // Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        // Apply effect to the player
        canDoubleJump = true;
        // Remove PowerUp 
        Destroy(gameObject);
    }
}
