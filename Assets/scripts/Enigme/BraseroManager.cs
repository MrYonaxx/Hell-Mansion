using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BraseroManager : MonoBehaviour
{ 

    [SerializeField]
    Brasero[] braseroToLit = null;
    [SerializeField]
    Brasero[] braseroToUnlit = null;

    [SerializeField]
    UnityEvent eventComplete = null;
    [SerializeField]
    UnityEvent eventUncomplete = null;

    bool conditionOn = false;


    private void Update()
    {
        for (int i = 0; i < braseroToLit.Length; i++)
        {
            if (braseroToLit[i].lit == false)
            {
                EventFailed();
                return;
            }
        }
        for (int i = 0; i < braseroToUnlit.Length; i++)
        {
            if (braseroToUnlit[i].lit)
            {
                EventFailed();
                return;
            }
        }

        if (conditionOn == false)
        {
            eventComplete.Invoke();
            conditionOn = true;
        }
    }

    void EventFailed()
    {
        if (conditionOn == true)
        {
            eventUncomplete.Invoke();
            conditionOn = false;
        }
    }


}
