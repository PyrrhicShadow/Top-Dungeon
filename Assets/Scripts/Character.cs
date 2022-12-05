using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Character
{
    public string name { get; set; } 
    public Color whoColor { get; set; } 
    public Color whatColor { get; set; } 
    public List<Sprite> sprites; 
    public Vector3 whoPos {get; set; } 
    public float speed { get; set; } 

    /// <summary>Just a name, normally for objects</summary>
    public Character(string name) 
    {
        InitChara(); 
        this.name = name; 
    }

    /// <summary>Name and dialogue speed, normally for special objects.</summary>
    public Character(string name, float speed) {
        InitChara(); 
        this.name = name; 
        this.speed = speed; 
    }

    /// <summary>Name and color, for npcs</summary>
    public Character(string name, Color whoColor) 
    {
        InitChara(); 
        this.name = name; 
        this.whoColor = whoColor; 

    }

    /// <summary>Name, color, and dialogue speed, for special npcs</summary>
    public Character(string name, Color whoColor, float speed) {
        InitChara(); 
        this.name = name; 
        this.whoColor = whoColor; 
        this.speed = speed; 
    }

    // Name, color, sprite, for characters with sprites
    public Character(string name, Color whoColor, List<Sprite> sprites) {
        InitChara(); 
        this.name = name; 
        this.whoColor = whoColor; 
        this.sprites = sprites; 
    }

    public Character(string name, Color whoColor, float speed, List<Sprite> sprites) {
        InitChara(); 
        this.name = name; 
        this.whoColor = whoColor; 
        this.speed = speed; 
        this.sprites = sprites; 
    }

    private void InitChara() {
        this.name = ""; 
        this.whoColor = Color.white; 
        this.whatColor = Color.white; 
        sprites = new List<Sprite>();
        this.speed = 0.5f; 
    }
}