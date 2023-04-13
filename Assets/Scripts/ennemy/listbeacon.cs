
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.Animations;

public class ListBeacon : MonoBehaviour
{
    public List<beacon> entities;
    public List<beacon> entitiessafe;
    
   

    void Awake()
    {
        
        entities = new List<beacon>();
        entitiessafe = new List<beacon>();
                         
        
    }

    void Update()
    {
        entitiessafe.Clear();

        foreach (beacon entity in entities)
        {
            if (!entity.Cover())
            {
                entitiessafe.Add(entity);
            }
            
            
        }
    }
}

