using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxShieldPoints = 3;
    public int maxHealthPoints = 3;
    public AudioClip hitSound;

    public HealthGUI HealthBar;
    public int currentHealth;
    public int currentShield;
    private Animator animator;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite shield;
    public Sprite shieldEmpty;
    public Image [] Heart;
    public Image [] Shield;

    private bool isVulnerable;
    public float invulnerailityTime;
    
    [Header("Debug")]
    public bool hideOnDeath = false;

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getCurrentShield()
    {
        return currentShield;
    }

    public void setCurrentShield(int newShield)
    {
        if(newShield < 0){
            currentShield = 0;
        }
        else
            currentShield = newShield;
        
    }
 
    public void setCurrentHealth(int newHealth)
    {
        if(newHealth < 0)
            currentHealth = 0;
        else
            currentHealth = newHealth;
        
    }
    void Start()
    {
        currentHealth = maxHealthPoints;
        isVulnerable = true;
        animator = GetComponent<Animator> ();
        currentShield = maxShieldPoints;
    }

    public void UpdateSprite()
    {
        for (int i = 0; i < Heart.Length; i++)
        {
            if(i< currentHealth){
                Heart[i].enabled = true;
            }
            else{
                Heart[i].enabled = false;
            }
            if(i< currentShield){
                Shield[i].enabled = true;
            }
            else{
                Shield[i].enabled = false;
            }
        }
    }
    public void TakeDamage(int amount)
    {

        if (currentHealth <= 0)
        return;

        
        if(isVulnerable)
        {
            Debug.Log("damage  Take");
            if(currentShield > 0)
            { // Gestion du bouclier
                currentShield -= amount;
                if (currentShield < 0)
                {
                    currentHealth += currentShield;
                    currentShield = 0;
                }
            }
            else
            {
                currentHealth -= amount;
            }
            
            AudioManager.Instance?.PlaySound(hitSound, 2);
            UpdateSprite();
            //HealthBar.setHealth(currentHealth); // Actualise la barre de vie
            GetComponent<FlashObject>().Flash();
            //GetComponent<PlayerControl>().audio.Play();
            if (currentHealth <= 0)
            {
                // we're dead
                animator.SetBool("Die", true);
                GetComponent<PlayerControl>().setAlive(false);
                //GetComponent<PlayerControl>().gameObject.SetActive(false);
            }
            else
            {
                isVulnerable = false;
                Invoke("ResetInvulnerability", invulnerailityTime);
            }
        }
     
    }
    
    public void ResetInvulnerability()
    {
        isVulnerable = true;
    }

    public void Revive()
    {
        setCurrentHealth(maxHealthPoints);
        UpdateSprite();
        animator.SetBool("Die", false);
        GetComponent<PlayerControl>().setAlive(true);
    }
}
