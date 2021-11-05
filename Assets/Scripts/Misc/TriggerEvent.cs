/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using Sirenix.OdinInspector;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    string tag;
    [SerializeField]
    UnityEvent triggerEvent;
    bool on = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tag) && on == false)
        {
            triggerEvent.Invoke();
            on = true;
            //this.gameObject.SetActive(false);
        }
    }

}