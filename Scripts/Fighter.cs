﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public fields 
    public int hitPoint;
    public int maxHitPoint;
    public float pushRecoverySpeed = 2.0f;

    // Immunity 
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push 
    protected Vector3 pushDirection; 

    // All fighters can ReceiveDamage or Die 
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText("-" + dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 15, 0.5f);
            Debug.Log("-" + dmg.damageAmount.ToString() + " to " + this.name); 
        }

        if (hitPoint <= 0)
        {
            hitPoint = 0;
            Death(); 
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject); 
    }
}
