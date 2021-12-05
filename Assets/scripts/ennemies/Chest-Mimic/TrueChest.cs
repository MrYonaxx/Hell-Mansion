using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class TrueChest : MonoBehaviour
{    
    
    private PlayerControl playerTrigger;
    private bool pickingItem;
    [SerializeField]
    private GameObject arm;
    protected bool isAnimationFinished;
    private bool isDone = false;
    public void Update()
    {
        if(!isDone)
        {
            if (pickingItem && Input.GetButton("Pick"))
            {
                
                gameObject.GetComponent<Animator>().SetBool("Open", true);
            }
            if(isAnimationFinished)
            {
                isDone = true;
                arm.SetActive(true);
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
