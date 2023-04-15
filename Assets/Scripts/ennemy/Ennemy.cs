using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    
    public beacon cover;
    public bool setcover;
    public NavMeshAgent ennemy;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
	public float health =100 ;
    
    // Patroling 
    public Vector3 walkPoint;
    public bool walkpointset;
    public float walkpointrange;
    
    //Attaking
    public float attackcd;
    private bool alreadyAttacked;
	public GameObject projectile;
    
    //State
    public float sightrange, attackrange;
    private bool playerIsInSightRange, playerIsInAttckRange;
    
    
    private void Start()
    {
        
        player = GameObject.Find("Astronaut").transform;
        ennemy = GetComponent<NavMeshAgent>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
	    playerIsInSightRange = Physics.CheckSphere(transform.position,sightrange,WhatIsPlayer);
	    playerIsInAttckRange = Physics.CheckSphere(transform.position,attackrange,WhatIsPlayer);	
	    player = GameObject.Find("Astronaut").transform;
	    if (health<51)
	    {
		     cover = ChoseCover();
		    if (setcover)
		    {
			    Vector3 distanceToWalkPoint = transform.position - cover.transform.position;
			    if (distanceToWalkPoint.magnitude < 0.2f)
			    {
				    transform.localScale = new Vector3(2, 2, 2);
				    if (playerIsInSightRange && playerIsInAttckRange)
				    { 
					    AttackPlayer();
				    }
				    transform.localScale = new Vector3(2, 1, 2);
				    
			    }
				    else
			    {
				    TakeCover();
			    }
			    
		    }
		    
	    }
	    else
	    {
		    if (playerIsInSightRange && !playerIsInAttckRange) ChasePlayer();
		    if (playerIsInSightRange && playerIsInAttckRange)
		    { 
			    AttackPlayer();
		    }
		    if (!playerIsInAttckRange && !playerIsInSightRange) Patroling();
	    }
		
        
            
        
        
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
       
		if (Physics.Raycast(walkPoint, -transform.up, 5f, WhatIsGround) )
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
		Vector3 playerPosition = player.position + Vector3.up * 2;
		transform.LookAt(playerPosition);
		

		if ( !alreadyAttacked)
		{
			Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
			
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
        health -= damage;
        
        if (health <= 0)
        {
            Destroy(gameObject);

        }
    }


   public beacon ChoseCover()
    {
        GameObject l = GameObject.Find("ListBeacon");
        ListBeacon List = l.GetComponent(typeof(ListBeacon)) as ListBeacon;
        if (List.entitiessafe.Count!=0)
        {
            beacon b = List.entities[0];
            float dist = Vector3.Distance(this.transform.position, b.transform.position);
            foreach (var v in List.entitiessafe)
            {
                float dist2 = Vector3.Distance(this.transform.position, v.transform.position);
                if (dist2 < dist)
                {
                    b = v;
                    dist = Vector3.Distance(this.transform.position, v.transform.position);
                }
            
            }

            setcover = true;
            return b;
            
        }

        setcover = false;
        return null;


    }

    public void TakeCover()
    {
        ennemy.SetDestination(cover.transform.position);
        
    }


}
  