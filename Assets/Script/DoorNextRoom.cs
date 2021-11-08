using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorNextRoom : MonoBehaviour
{
    [SerializeField]
    UnityEvent eventOpen;
    bool onTrigger = false;



    // Update is called once per frame
    void Update()
    {
        if(onTrigger) 
        {
            /*if(Input.GetMouseButtonDown(0))
            {
                eventOpen.Invoke();
            }*/
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            eventOpen.Invoke();
            onTrigger = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }
}
