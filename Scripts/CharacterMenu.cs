using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    public Text levelText, hitPointText, soulsText, upgradeCostText, xpText; 

    // Logic
    public static bool menu = false; 
    public Animator animator; 
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Pause/unpause game when entering menu
    public void PauseGame(bool pause)
    {
        if (pause)
        {
            menu = true; 
            Time.timeScale = 0.0f;
        }
        else
        {
            menu = false; 
            Time.timeScale = 1.0f;
        }
    }

    // Keymap the game menu 
    public void Update() 
    {
        if (Input.GetButtonDown("Cancel")) {
            if (menu) 
            {
                animator.SetTrigger("hide"); 
                PauseGame(!menu); 
            }
            else 
            {
                animator.SetTrigger("show"); 
                UpdateMenu();
                PauseGame(!menu); 
            }
        }
    }

    // Character Selection
    public void OnArrowClick(bool right)
    {
        
        if (right) 
        {
            currentCharacterSelection++; 

            // If we went too far away 
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count) 
            {
                currentCharacterSelection = 0; 
            }
            OnSelectionChanged(); 
        }
        else 
        {
            currentCharacterSelection--; 

            // If we went too far away 
            if (currentCharacterSelection < 0) 
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1; 
            }

            OnSelectionChanged(); 
        }

    }
    private void OnSelectionChanged() 
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection]; 
        GameManager.instance.player.SwapSprite(currentCharacterSelection); 
    }

    // Weapon Upgrade 
    public void OnUpgradeClick() 
    {
        // Game manager deals with weapon 
        if(GameManager.instance.TryUpgradeWeapon()) 
        {
            UpdateMenu(); 
        }
    }

    // Update the character information 
    public void UpdateMenu() 
    {
        Debug.Log("Pausing game.");

        // Weapon 
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel]; 
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count) 
        {
            upgradeCostText.text = "MAX"; 
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString() + " souls"; 
        }

        // Meta 
        hitPointText.text = GameManager.instance.player.hitPoint.ToString() + " / " + GameManager.instance.player.maxHitPoint.ToString(); 
        soulsText.text = GameManager.instance.souls.ToString(); 
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();  

        // xp Bar 
        int currentLevel = GameManager.instance.GetCurrentLevel(); 
        if (currentLevel == GameManager.instance.xpTable.Count) 
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points."; // Display total xp 
            xpBar.localScale = Vector3.one; 
        }
        else 
        {
            int previousLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1); 
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel); 

            int diff = currentLevelXp - previousLevelXp; 
            int xpIntoLevel = GameManager.instance.experience - previousLevelXp; 

            float completionRatio = (float)xpIntoLevel / (float)diff; 

            xpText.text = xpIntoLevel.ToString() + " / " + diff.ToString(); 
            xpBar.localScale = new Vector3(completionRatio, 1.0f, 1.0f); 
        }

    }
}
