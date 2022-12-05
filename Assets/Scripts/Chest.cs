using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite openChest; 
    public Sprite emptyChest;
    public int soulsAmount = 10; 

    protected override void OnCollect()
    {

        if (!collected)
        {
            base.OnCollect();
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.souls += soulsAmount;
            Debug.Log("+" + soulsAmount + " souls!"); 
            GameManager.instance.ShowText("+" + soulsAmount + " souls!", 25, Color.yellow, transform.position, Vector3.up * 25, 1.5f); // from Color(0, 0, 0) to Color(1, 1, 1) 
        }
    }
}
