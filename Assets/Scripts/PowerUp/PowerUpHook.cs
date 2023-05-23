using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHook : MonoBehaviour
{
	public GameObject pickupEffect;
    public GameObject Panel;
    private bool test = false;


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
        // Apply effect to the player
        Hook.HaveHook = true;
        Destroy(gameObject);
    }
}

