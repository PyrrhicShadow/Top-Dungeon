﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    public GameObject dialogue; 
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10]; 

    protected bool showing = false; 

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); 
    }

    protected virtual void Update()
    {
        // Collision work 
        boxCollider.OverlapCollider(filter, hits); 
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue; 
            }

            OnCollide(hits[i]); 

            // The array is not cleaned up, so we do it ourself 
            hits[i] = null; 
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was not implemented in " + this.name); 
    }
}
