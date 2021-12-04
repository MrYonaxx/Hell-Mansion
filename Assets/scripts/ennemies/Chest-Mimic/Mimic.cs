using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{    
    private PlayerControl playerTrigger;
    private bool pickingItem;
    [SerializeField]
    private GameObject arm;
    protected bool isAnimationFinished;
    public void Update()
    {
        {
            if (pickingItem && Input.GetButton("Pick"))
            {
                //TODO Activer un ennemi mimic qui explose
                // Destroy le coffre pour une parfaite transition
                // Ou le faire peter sur place
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        playerTrigger = other.GetComponent<PlayerControl>();
            
        if (playerTrigger != null)
        {
            playerTrigger.hud.OpenMessagePanel();
            pickingItem = true;
        };
    }

    private void OnTriggerExit(Collider other)
    {
        playerTrigger= other.GetComponent<PlayerControl>();

        if (playerTrigger != null)
        {
            pickingItem = false;
            playerTrigger.hud.CloseMessagePanel();
        } 
    }

     public virtual void triggerOpen()
    {
        isAnimationFinished = false;
    }
    public virtual void finishOpen()
    {
        isAnimationFinished = true;
    }
}
