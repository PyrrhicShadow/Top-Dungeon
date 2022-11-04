using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] orbitSpeeds = {2.5f, -2.5f}; 
    public float distance = 0.25f; 
    public Transform[] orbits; 

    private void Update() 
    {
        for (int i = 0; i < orbits.Length; i++) 
        {
            orbits[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * orbitSpeeds[i]) * distance, Mathf.Sin(Time.time * orbitSpeeds[i]) * distance, 0); 
        }
    }
}
