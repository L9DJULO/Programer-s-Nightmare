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
	public GameObject projectile2;

	public bool grenade = false;
    
    //State
    public float sightrange, attackrange;
    private bool playerIsInSightRange, playerIsInAttckRange;
    
    
    private void Awake()
    {
        
        
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
		     Vector3 distanceToWalkPoint = transform.position - cover.transform.position;

		     if (distanceToWalkPoint.magnitude < 2f && !alreadyAttacked)
		     {
			     transform.localScale = new Vector3(1.5f, 1f, 1.5f);
			     Invoke(nameof(AttackPlayer), 0.5f);
		     }
		     else
		     {
			     TakeCover();
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
		Vector3 playerPosition = player.position + Vector3.up;
		transform.LookAt(playerPosition);
		

		if ( !alreadyAttacked)
		{
			if (clear())
			{
				Vector3 tire = transform.position + transform.up*1f;
				if (grenade)
				{
					Rigidbody rb = Instantiate(projectile2, tire, Quaternion.identity).GetComponent<Rigidbody>();
					rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
					rb.AddForce(transform.up * 8f, ForceMode.Impulse);
				}
				else
				{
					Rigidbody rb = Instantiate(projectile, tire, Quaternion.identity).GetComponent<Rigidbody>();
					rb.AddForce(transform.forward * 40f, ForceMode.Impulse);
				}

				if (health < 51)
				{
					transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				}
				alreadyAttacked = true;
				Invoke(nameof(ResetAttack), attackcd);
			}
			else
			{
				ChasePlayer();
			}

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
            beacon b = List.entitiessafe[0];
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
	    if (cover!=null)
	    {
		    ennemy.SetDestination(cover.transform.position);

	    }
        
    }
    public bool clear()
    {
        
            
	    Vector3 direction = player.position -  (transform.position - transform.up)  ;

            
	    RaycastHit hit;
	    if (Physics.Raycast(transform.position+transform.up, direction, out hit))
	    {
               
		    if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Untagged") )
		    {
			    return false;
		    }
	    }

	    return true;


    }


}
  