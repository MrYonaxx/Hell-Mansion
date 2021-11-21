using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class WaitZone : MonoBehaviour
{
    bool inWaitZone = false;

    [SerializeField]
    float timeToWait = 3f;
    [SerializeField]
    UnityEvent eventWait;

    float t = 0f;

    private void Update()
    {
        if(inWaitZone)
        {
            t += Time.deltaTime;
            if (t > timeToWait)
                eventWait.Invoke();
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inWaitZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inWaitZone = false;
            t = 0f;
        }
    }
}
