using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takegun : MonoBehaviour
{
    public GameObject takgun;
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
        yield return new WaitForSeconds(5);
        Panel.SetActive(false);
        Pickup(other);
    }
    void Pickup(Collider player)
    {
        // Spawn a cool effect
        // Apply effect to the player
        Weaponappear.Haveweapon = true;
        // Remove PowerUp 
        Destroy(takgun);
    }
}
