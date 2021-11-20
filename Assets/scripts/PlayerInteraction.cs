using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    PlayerControl player;
    
    List<IInteractable> interactables = new List<IInteractable>();
    IInteractable objInteractable = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && objInteractable != null)
        {
            objInteractable.Interact(player);

            if (objInteractable.OnlyOnce())
            {
                objInteractable.CanInteract(false);
                interactables.Remove(objInteractable);
                CheckShortestInteractable();
            }
        }
    }


    // On check l'interactabe le plus proche du joueur et on le set en temps qu'objInteractable
    void CheckShortestInteractable()
    {
        if (objInteractable != null)
            objInteractable.CanInteract(false);

        if (interactables.Count == 0)
        {
            objInteractable = null;
            return;
        }
        else if (interactables.Count == 1)
        {
            objInteractable = interactables[0];
        }
        else
        {
            float distanceMin = 9999;
            float distance = 0;
            for (int i = 0; i < interactables.Count; i++)
            {
                distance = (this.transform.position - interactables[i].GetPos()).magnitude;
                if(distance < distanceMin)
                {
                    distance = distanceMin;
                    objInteractable = interactables[i];
                }
            }
        }
        objInteractable.CanInteract(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactables.Add(interactable);
            CheckShortestInteractable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if(interactables.Contains(interactable))
            {
                interactable.CanInteract(false);
                interactables.Remove(interactable);
                CheckShortestInteractable();
            }
        }
    }
}
