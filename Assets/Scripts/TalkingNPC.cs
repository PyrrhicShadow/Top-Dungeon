using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : Collidable
{
    [SerializeField] string msg; 
    [SerializeField] Color color = Color.white; 
    [SerializeField] float duration = 4.0f; 

    private float cooldown; 
    private float lastShout; 

    public List<Say> stuff = new List<Say>()
    {
        
    }; 
    // private Character me = GameManager.instance.characters["tutorial"];

    protected override void Start() 
    {
        base.Start(); 
        InitDialogue(); 
        cooldown = duration; 
        lastShout = -cooldown; 
    }

    private void InitDialogue() {
        Character me = new Character("Tutorial", this.gameObject.GetComponent<SpriteRenderer>().sprite); 
        Character you = new Character("You", Color.cyan); 

        stuff.Add(new Say(me, "Welcome to the course! Try hitting the boxes with your sword!")); 
        stuff.Add(new Say(new Character("You"), "Okay.")); 
    }

    protected override void OnCollide(Collider2D coll)
    {
        /* if (Time.time - lastShout > cooldown) {
            lastShout = Time.time; 
            GameManager.instance.ShowText(msg, 12, color, transform.position + Vector3.up * 0.16f, Vector3.zero, duration); 
        }*/ 

        if (Input.GetButtonDown("Submit"))
        {
            if (!showing && Time.time - lastShout > cooldown) 
            {
                dialogue.GetComponent<DialogueManager>().Say(stuff); 
                // dialogue.GetComponent<Animator>().SetTrigger("show"); 
                showing = true;
            }
            else 
            {
                // dialogue.GetComponent<Animator>().SetTrigger("hide"); 
                showing = false; 
            }
            lastShout = Time.time; 
        }
    }
}
