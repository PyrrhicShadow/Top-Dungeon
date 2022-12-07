using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob
{

    private SpriteRenderer spriteRenderer; 
    public bool isAlive { get; set; } = true; 

    protected override void Start()
    {
        base.Start(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
        }
    }

    public void SwapSprite(int skinID) {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID]; 
    }

    public void OnLevelUp() 
    {
        maxHitPoint++; 
        hitPoint = maxHitPoint; 
    }

    public void SetLevel(int level) 
    {
        for (int i = 1; i < level; i++) 
        {
            OnLevelUp(); 
        }
    }

    public void Heal(int healAmount) 
    {
        if (hitPoint == maxHitPoint) 
        {
            return; 
        }

        hitPoint += healAmount; 

        if (hitPoint > maxHitPoint) 
        {
            hitPoint = maxHitPoint; 
        }
        
        GameManager.instance.ShowText("+" + healAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 25, 1.0f); 
        GameManager.instance.OnHitPointChange(); 
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (isAlive) 
        {
            base.ReceiveDamage(dmg);
            GameManager.instance.OnHitPointChange(); 
        }
    }

    protected override void Death()
    {
        GameManager.instance.ShowText("This is not the end...!", 25, Color.gray, transform.position, Vector3.up * 40, 3.0f);
        isAlive = false;  
        GameManager.instance.deathMenuAnim.SetTrigger("show"); 
        Debug.Log("Show death menu"); 

    }

    public void Respawn() 
    {
        hitPoint = maxHitPoint; 
        isAlive = true; 
        Debug.Log("isAlive set to " + isAlive.ToString()); 
        lastImmune = Time.time; 
        pushDirection = Vector3.zero; 
    }
}
