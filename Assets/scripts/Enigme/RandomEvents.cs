using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent[] eventPlayer;

    public void appelRandom()
    {
        eventPlayer[Random.Range(0, eventPlayer.Length)].Invoke();
    }
}
