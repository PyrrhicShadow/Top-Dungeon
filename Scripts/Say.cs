
public class Say
{
    public Character who; 
    public string what; 

    public Say(Character who, string what) 
    {
        this.who = who; 
        this.what = what; 
    } 

    public Say(string who, string what) 
    {
        this.who = new Character(who);  
        this.what = what; 
    }
}
