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
    public int currentHealth;
    public int currentShield;
    private Animator animator;
    
    public Image [] Heart;
    public Image [] Shield;

    private bool isVulnerable;
    public float invulnerailityTime;
    
    public GameOverHUD GameOver;

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getCurrentShield()
    {
        return currentShield;
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
            GetComponent<FlashObject>().Flash(invulnerailityTime);
            //GetComponent<PlayerControl>().audio.Play();
            if (currentHealth <= 0)
            {
                // we're dead
                animator.SetBool("Die", true);
                GetComponent<PlayerControl>().setAlive(false);
                GameOver.OnDeath();
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
        currentHealth = maxHealthPoints;
        UpdateSprite();
        animator.SetBool("Die", false);
        GetComponent<PlayerControl>().setAlive(true);
    }


    public void addShield() // Add 1 shieldPoint
    {
        if(currentShield  < currentHealth && currentShield < maxShieldPoints)
        {
            currentShield += 1;
            UpdateSprite();
        }
    }
}
