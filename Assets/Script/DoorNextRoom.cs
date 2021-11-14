using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorNextRoom : MonoBehaviour
{
    [SerializeField]
    UnityEvent eventOpen;
    bool onTrigger = false;


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
