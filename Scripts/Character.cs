using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string name; 
    public Color whoColor; 
    public Color whatColor; 
    public List<Sprite> sprites; 
    public Vector3 whoPos; 

    // Just a name, normally for objects
    public Character(string name) 
    {
        this.name = name; 
        this.whoColor = Color.white; 
        this.whatColor = Color.white; 
        // this.sprites = sprites; 
    }

    // Name and color, for npcs
    public Character(string name, Color whoColor) 
    {
        this.name = name; 
        this.whoColor = whoColor; 
        this.whatColor = Color.white; 
        // this.sprites = sprites; 
    }

    // Name, color, sprite, for characters with sprites
    public Character(string name, Color whoColor, List<Sprite> sprites) {
        this.name = name; 
        this.whoColor = whoColor; 
        this.whatColor = Color.white; 
        // this.sprites = sprites; 
    }
}