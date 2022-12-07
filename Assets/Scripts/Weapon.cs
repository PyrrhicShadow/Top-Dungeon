using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage structure 
    [SerializeField] int[] damagePoint = {1, 2, 3, 4, 5, 6, 7};
    [SerializeField] float[] pushForce = {2.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.6f, 4f};

    // Upagrade 
    public int weaponLevel { get; set; } = 0;
    private SpriteRenderer spriteRenderer;

    // Swing 
    private Animator anim; 
    private float cooldown = 0.5f;
    private float lastSwing; 

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>(); 
    }

    protected override void Update()
    {
        if (GameManager.instance.player.isAlive) 
        {
            base.Update(); 
            if (Input.GetButtonDown("Fire1"))
            {
                if (Time.time - lastSwing > cooldown)
                {
                    lastSwing = Time.time;
                    Swing(); 
                }
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name != "player")
        {
            // Create a new Damage object, then we'll send it to the Fighter we've hit 
            Damage dmg = new Damage(transform.position, damagePoint[weaponLevel], pushForce[weaponLevel]); 

            coll.SendMessage("ReceiveDamage", dmg); 
        }
    }

    private void Swing()
    {
        // GameManager.instance.ShowText("Swoosh!", 25, new Color(0.5f, 0.5f, 0.5f), transform.position, Vector3.right * 25, 0.5f); // from Color(0, 0, 0) to Color(1, 1, 1) 
        anim.SetTrigger("Swing"); 
    }

    public void UpgradeWeapon() 
    {
        weaponLevel++; 
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel]; 

        // change stats %% 
    }

    public void SetWeaponLevel(int level) 
    {
        weaponLevel = level; 
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel]; 
    }
}
