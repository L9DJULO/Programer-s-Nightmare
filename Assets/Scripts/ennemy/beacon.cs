using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.Animations;
public class beacon : MonoBehaviour
{
    public Transform player;
    public Transform beac;
    public ListBeacon List;

    
    // Start is called before the first frame update
    void Start()
    {
        
        
        GameObject l = GameObject.Find("ListBeacon");
        List = l.GetComponent(typeof(ListBeacon)) as ListBeacon;
        List.entities.Add(this);
        beac = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
		
       player = ChoseJ().transform;
    }

    public bool Cover()
    {
        
            
            Vector3 direction = player.position - beac.position;

            
            RaycastHit hit;
            if (Physics.Raycast(beac.position, direction, out hit))
            {
               
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Untagged"))
                {
                    return true;
                }
            }

            return false;


    }
    
    public playermovsolo ChoseJ()
    {
	    GameObject l = GameObject.Find("ListBeacon");
	    ListBeacon List = l.GetComponent(typeof(ListBeacon)) as ListBeacon;
	    if (List.joueurs.Count!=0)
	    {
		    playermovsolo b = List.joueurs[0];
		    float dist = Vector3.Distance(this.transform.position, b.transform.position);
		    foreach (var v in List.joueurs)
		    {
	           
			    float dist2 = Vector3.Distance(this.transform.position, v.transform.position);
			    if (dist2 < dist)
			    {
				    b = v;
				    dist = Vector3.Distance(this.transform.position, v.transform.position);
			    }
            
		    }
		    
		    return b;
            
	    }

	  
	    return null;


    }


}
