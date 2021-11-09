using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class Health : MonoBehaviour
{
    public int maxHealthPoints = 3;
    public AudioClip hitSound;

    
    public int currentHealth;
    private Animator animator;

    [Header("Debug")]
    public bool hideOnDeath = false;

    public int getCurrentHealth()
    {
        return currentHealth;
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
        animator = GetComponent<Animator> ();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
        return;

        Debug.Log("damage  Take");
        currentHealth -= amount;
        AudioManager.Instance?.PlaySound(hitSound, 2);
        if (GetComponent<PlayerControl>())
        {
            GetComponent<FlashObject>().Flash();
            //GetComponent<PlayerControl>().audio.Play();
            if (currentHealth <= 0)
            {
                // we're dead
                animator.SetBool("Die", true);
                GetComponent<PlayerControl>().setAlive(false);
                //GetComponent<PlayerControl>().gameObject.SetActive(false);
            }
        } 
        else
        {
            if (currentHealth <= 0)
            {
                if (hideOnDeath)
                    this.gameObject.SetActive(false);
            }
        }
    }


    public void Revive()
    {
        setCurrentHealth(maxHealthPoints);
        if (GetComponent<PlayerControl>())
        {
            animator.SetBool("Die", false);
            GetComponent<PlayerControl>().setAlive(true);
            //GetComponent<PlayerControl>().gameObject.SetActive(false);
        }
    }
}
