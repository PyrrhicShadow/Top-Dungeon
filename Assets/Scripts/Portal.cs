using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] string[] sceneNames; 

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "player")
        {
            // Save the game 
            GameManager.instance.SaveState(); 

            // Teleport the player
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            Debug.Log("Teleported player to " + sceneName); 
        }
    }
}
