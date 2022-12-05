using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{
    // Text fields
    [SerializeField] Text who; 
    [SerializeField] Text what; 
    [SerializeField] Image whoSprite; 
    [SerializeField] GameObject sayContainer; 
    private Animator animator; 
    private float saySpeed; 
    private List<Character> charas = new List<Character>();
    private int index = -1; 
    private List<Say> dialogue; 

    void Start() {
        animator = this.gameObject.GetComponent<Animator>(); 
        who.text = ""; 
        what.text = ""; 
    }

    void Update() {
        if (Input.GetButtonDown("Submit") && index != -1) {
            if (what.text == dialogue[index].what) {
                NextLine(); 
            }
            else {
                StopAllCoroutines(); 
                what.text = dialogue[index].what; 
                Debug.Log("Line quick-completed."); 
            }
        }
    }

    public void Say(List<Say> dialogue) 
    {
        this.dialogue = dialogue; 
        animator.SetTrigger("show"); 
        Time.timeScale = 0f; 
        index = 0; 
        SayLine(dialogue[index]); 
        StartCoroutine(TypeLine(dialogue[index])); 
    }

    private IEnumerator TypeLine(Say line) 
    {
        Debug.Log("Start line."); 
        // type each character 1 by 1
        foreach (char c in line.what) {
            what.text += c; 
            yield return new WaitForSecondsRealtime(line.who.speed); 
        }
        Debug.Log("Line complete."); 
    }

    private void NextLine() {
        if (index < dialogue.Count - 1) {
            index++; 
            what.text = ""; 
            SayLine(dialogue[index]); 
            StartCoroutine(TypeLine(dialogue[index])); 
        }
        else {
            animator.SetTrigger("hide"); 
            Time.timeScale = 1f; 
            index = -1; 
        }
    }

    private void SayLine(Say line) {
        who.text = line.who.name; 
        who.color = line.who.whoColor; 
        what.color = line.who.whatColor;
        what.text = "";  
    }
}
