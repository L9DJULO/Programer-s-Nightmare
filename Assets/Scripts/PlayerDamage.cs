using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public HealthBarScript healthBar;
    public static float Health = 100;
    public static float maxHealth = 100;
    private Rigidbody rb;
    private Vector3 oldPosition;
    private bool BossIsNear = false;
    public LayerMask WhatIsBoss;
    public LayerMask SafeZone;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldPosition =rb.position ;
        Health = maxHealth;
    }

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, 5, SafeZone))
        {
            if (Health < maxHealth)
                Health += 30;
            if (Health > maxHealth)
                Health = maxHealth;
            healthBar.setHealth(Health);
        }
        if (Physics.CheckSphere(transform.position, 2, WhatIsBoss))
            TakeDamage(0.5f);
    }

    public void TakeDamage(float damage)
    {
        if (!Physics.CheckSphere(transform.position, 5, SafeZone))
            Health -= damage;
        healthBar.setHealth(Health); 
        if (Health <= 0)
        {
            rb.position = oldPosition;
            Health = 100;
            healthBar.setHealth(Health);
        }
    }
}
