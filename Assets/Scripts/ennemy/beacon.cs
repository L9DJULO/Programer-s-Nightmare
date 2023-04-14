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
        
        Debug.Log("yo");
        GameObject l = GameObject.Find("ListBeacon");
        List = l.GetComponent(typeof(ListBeacon)) as ListBeacon;
        List.entities.Add(this);
        beac = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
		
        player = GameObject.Find("Astronaut").transform;
    }

    public bool Cover()
    {
        
            
            Vector3 direction = player.position - beac.position;

            
            RaycastHit hit;
            if (Physics.Raycast(beac.position, direction, out hit))
            {
               
                if (hit.collider.CompareTag("Wall"))
                {
                    return true;
                }
            }

            return false;


    }
    
}
