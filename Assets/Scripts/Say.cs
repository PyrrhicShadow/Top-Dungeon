using UnityEngine; 

public class Say
{
    public Character who { get; set; } 
    public Sprite sprite { get; set; }
    public string what { get; set; } 

    public Say(Character who, string what) 
    {
        this.who = who; 
        this.what = what; 
        if (this.who.sprites.Count > 0) {
            this.sprite = this.who.sprites[0]; 
        }
    } 

    public Say(string who, string what) 
    {
        this.who = new Character(who);  
        this.what = what; 
        if (this.who.sprites.Count > 0) {
            this.sprite = this.who.sprites[0]; 
        }
    }

    public Say(Character who, string what, int sprite) {
        this.who = who; 
        this.what = what; 
        if (this.who.sprites.Count > 0) {
            this.sprite = this.who.sprites[sprite]; 
        }
    }
}
