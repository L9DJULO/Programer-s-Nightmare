using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWallRun : MonoBehaviour
{
    public static bool HaveWallRun =false;
    public GameObject pickupEffect;
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
        yield return new WaitForSeconds(2);
        Panel.SetActive(false);
        Pickup(other);
    }

    void Pickup(Collider Player)
    {
        // Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        // Apply effect to the player
        HaveWallRun = true;
		Destroy(gameObject);
    }
}