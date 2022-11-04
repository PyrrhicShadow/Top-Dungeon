using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{
    // Text fields
    public Text who, what; 
    public Image whoSprite; 
    private float sayDur; 
    private List<Character> charas = new List<Character>();

    public void Say(List<Say> dialogue) 
    {
        foreach (Say line in dialogue)
        {
            who.text = line.who.name; 
            Debug.Log(line.who.name + "'s line."); 
            who.color = line.who.whoColor; 
            // whoSprite.sprite = do something

            what.text = line.what; 
            what.color = line.who.whatColor; 

            sayDur = (float)line.what.Length; 
            SayWait(sayDur); 
        }
    }

    private IEnumerator SayWait(float sayDur) 
    {
        Debug.Log("Showing line."); 
        yield return new WaitForSeconds(sayDur); 
        Debug.Log("Moving on to next line"); 
    }
}
