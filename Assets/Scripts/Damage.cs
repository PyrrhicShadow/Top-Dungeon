using UnityEngine;

public struct Damage
{
    public Vector3 origin { get; private set; }
    public int damageAmount { get; private set; }
    public float pushForce { get; private set; } 

    public Damage(Vector3 origin, int damageAmount, float pushForce) {
        this.origin = origin; 
        this.damageAmount = damageAmount; 
        this.pushForce = pushForce; 
    }
}
