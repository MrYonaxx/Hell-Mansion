using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{    
    private PlayerControl playerTrigger;
    private bool pickingItem;
    [SerializeField]
    private GameObject mimic;

        [SerializeField]
    private GameObject chest;
    protected bool isAnimationFinished;
    private bool isDone = false;
    public void Update()
    {
        
        if (pickingItem && Input.GetButton("Pick"))
        {
            //TODO Activer un ennemi mimic qui explose
            // Destroy le coffre pour une parfaite transition
            // Ou le faire peter sur place
        }
        if(!isDone)
        {
            if (pickingItem && Input.GetButton("Pick"))
            {
                
                gameObject.GetComponent<Animator>().SetBool("Open", true);
            }
            if(isAnimationFinished)
            {
                isDone = true;
                Destroy(chest);
                mimic.SetActive(true);

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
