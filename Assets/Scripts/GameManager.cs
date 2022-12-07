using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject); 
            Destroy(floatingTextManager.gameObject); 
            Destroy(hud); 
            Destroy(menu); 
            Destroy(settings); 
            Destroy(dialogue); 
            return; 
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState; 
        SceneManager.sceneLoaded += OnSceneLoaded; 

        // Characters 
        // characters = new Dictionary<string, Character>(); 
        // characters.Add("player", new Character("player", Color.white)); 
        // characters.Add("tutorial", new Character("tutorial")); 
    }

    // Resources 
    [SerializeField] List<Sprite> _playerSprites;
    public List<Sprite> playerSprites { get { return _playerSprites; } private set { _playerSprites = value;} }
    [SerializeField] List<Sprite> _weaponSprites;
    public List<Sprite> weaponSprites {get { return _weaponSprites; } private set { _weaponSprites = value; } } 
    [SerializeField] List<int> _weaponPrices;
    public List<int> weaponPrices { get { return _weaponPrices; } private set { _weaponPrices = value; } }
    [SerializeField] List<int> _xpTable; 
    public List<int> xpTable { get { return _xpTable; } private set { _xpTable = value; } }
    public Dictionary<string, Character> charas = new Dictionary<string, Character>() 
    {
        {"player", new Character("You", Color.white)}, 
        {"tutorial", new Character("Tutorial NPC")}
    }; 

    // References 
    [SerializeField] Player _player; 
    public Player player { get { return _player; } private set { _player = value; } }
    [SerializeField] Weapon _weapon; 
    public Weapon weapon { get { return _weapon; } private set { _weapon = value; } }
    [SerializeField] FloatingTextManager floatingTextManager;
    [SerializeField] RectTransform hitPointBar; 
    [SerializeField] Animator _deathMenuAnim; 
    public Animator deathMenuAnim { get { return _deathMenuAnim; } private set { _deathMenuAnim = value; } } 
    [SerializeField] Animator settingsMenuAnim; 
    [SerializeField] GameObject hud; 
    [SerializeField] GameObject menu; 
    [SerializeField] GameObject settings; 
    [SerializeField] GameObject dialogue; 

    // Logic 
    public int souls { get; set; }
    public int experience { get; set; }
    
    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) 
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration); 
    }

    // Upgrade Weapon 
    public bool TryUpgradeWeapon() 
    {
        // is the weapon max level yet? 
        if (weapon.weaponLevel >= weaponPrices.Count) 
        {
            Debug.Log("Weapon at max level."); 
            return false; 
        }

        if (souls >= weaponPrices[weapon.weaponLevel])
        {
            souls -= weaponPrices[weapon.weaponLevel]; 
            weapon.UpgradeWeapon(); 
            Debug.Log("Weapon upgraded."); 
            return true; 
        }

        Debug.Log("Insuficient souls."); 
        return false; 
    }

    // Hp Bar 
    public void OnHitPointChange() 
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint; 
        hitPointBar.localScale = new Vector3(1, ratio, 1); 
    }

    // xp System 
    public int GetCurrentLevel() 
    {
        int r = 0; 
        int add = 0; 

        while (experience >= add) 
        {
            add += xpTable[r]; 
            r++; 
            
            if (r == xpTable.Count) // Max Level 
            {
                return r; 
            }
        }

        return r; 
    }
    public int GetXpToLevel(int level) 
    {
        int r = 0; 
        int xp = 0; 

        while (r < level) 
        {
            xp += xpTable[r]; 
            r++; 
        }

        return xp; 
    }
    public void GrantXp(int xp) 
    {
        int currentLevel = GetCurrentLevel(); 
        experience += xp; 
        if (currentLevel < GetCurrentLevel()) 
        {
            OnLevelUp(); 
        }
    }
    public void OnLevelUp() 
    {
        player.OnLevelUp(); 
        OnHitPointChange(); 
        souls += 10; 
        Debug.Log("Level up!"); 
    }

    // On Scene Loaded 
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("spawnPoint").transform.position; 
    }

    // Death Menu and Respawn 
    public void Respawn() 
    {
        deathMenuAnim.SetTrigger("hide"); 
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); 
        player.Respawn(); 
    }

    // Save/load state
    /**
     * INT peferedSkin 
     * INT souls 
     * INT experience 
     * INT weaponLevel 
     */ 
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += souls.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString(); 

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SaveState"); 
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return; 
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // change player skin 
        souls = int.Parse(data[1]);
        experience = int.Parse(data[2]); 
        player.SetLevel(GetCurrentLevel()); 
        weapon.SetWeaponLevel(int.Parse(data[3])); 

        Debug.Log("LoadState"); 
    }

    public void DeleteSave() 
    {
        PlayerPrefs.DeleteAll(); 
        settingsMenuAnim.SetTrigger("hide"); 
        Time.timeScale = 1.0f; 
    }

}
