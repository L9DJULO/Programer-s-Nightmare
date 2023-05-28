using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool alreadyAttacked;

    public float attackcd;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
     alreadyAttacked = false;
     attackcd = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackPlayer();
        }
    }
    
    private void AttackPlayer()
    {

        
        if ( !alreadyAttacked)
        {

            Vector3 tire = transform.position + transform.up * 2f;

                    Rigidbody rb = Instantiate(projectile, tire, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 40f, ForceMode.Impulse);
                
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), attackcd);
        }


    }
    

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
