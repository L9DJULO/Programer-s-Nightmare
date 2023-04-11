using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    
    
    public NavMeshAgent ennemy;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
	public float heath =100 ;
    
    // Patroling 
    public Vector3 walkPoint;
    private bool walkpointset;
    public float walkpointrange;
    
    //Attaking
    public float attackcd;
    private bool alreadyAttacked;
	public GameObject projectile;
    
    //State
    public float sightrange, attackrange;
    private bool playerIsInSightRange, playerIsInAttckRange;
    
    
    private void Awake()
    {
        
        player = GameObject.Find("Orientation").transform;
        ennemy = GetComponent<NavMeshAgent>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        playerIsInSightRange = Physics.CheckSphere(transform.position,sightrange,WhatIsPlayer);
        playerIsInAttckRange = Physics.CheckSphere(transform.position,attackrange,WhatIsPlayer);	
            
        if (playerIsInSightRange && !playerIsInAttckRange)
            ChasePlayer();
        if (playerIsInSightRange && playerIsInAttckRange)
        {
            
                AttackPlayer();
        }
        if (!playerIsInAttckRange && !playerIsInSightRange)
            ennemy.SetDestination(transform.position);
        
    }

    private void Patrol()
    {
        if (!walkpointset) searchwalkpoint();
            
    }
    private void searchwalkpoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
		float randomX = Random.Range(-walkpointrange, walkpointrange);
		
		walkPoint = new Vector3(transform.position.x + randomX,transform.position.y , transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatIsGround) )
		{
			walkpointset = true;
		}

    }

	private void Patroling()
	{
		if (!walkpointset) searchwalkpoint();

		if (walkpointset)
			ennemy.SetDestination(walkPoint);	

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		if ( distanceToWalkPoint.magnitude < 1f)
			walkpointset = false;
	}

	private void ChasePlayer()
   	 	{
        	ennemy.SetDestination(player.position);
    	}

	private void AttackPlayer()
	{
		ennemy.SetDestination(transform.position);
		transform.LookAt(player);

		if ( !alreadyAttacked)
		{
			Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
			rb.AddForce(transform.up * 8f, ForceMode.Impulse);
			alreadyAttacked = true;
			Invoke(nameof(ResetAttack), attackcd);
		}
	}

	private void ResetAttack()
	{
		alreadyAttacked = false;
	}
		
	public void TakeDamage(float damage)
    {
        heath -= damage;
        Debug.Log("Enemy toucher ");
        if (heath <= 0)
        {
            Destroy(gameObject);

        }
    }
}
  