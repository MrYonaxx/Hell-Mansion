using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    
    
    
    private bool pickingItem;
    private Health playerHealth;
    private PlayerControl playerTrigger;
     private void Update()
    {
        if (pickingItem && Input.GetButton("Pick"))
        {
            Destroy(gameObject);
            playerHealth.addShield();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        playerTrigger = other.GetComponent<PlayerControl>();
        playerHealth = other.GetComponent<Health>();
        if (playerTrigger != null)
        {
            playerTrigger.hud.OpenMessagePanel();
            pickingItem = true;
        };
    }

    private void OnTriggerExit(Collider other)
    {
        playerTrigger= other.GetComponent<PlayerControl>();
        playerHealth = other.GetComponent<Health>();

        if (playerTrigger != null)
        {
            pickingItem = false;
            playerTrigger.hud.CloseMessagePanel();
        } 
    }
}
