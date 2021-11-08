using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    [SerializeField]
    public UnityEvent eventStartRoom;

    [HideInInspector]
    public UnityEvent eventEndRoom;

    [SerializeField]
    private Transform startPosition;
    public Transform StartPosition
    {
        get { return startPosition; }
    }


    // Fonction initialisant la salle
    public virtual void StartRoom()
    {
        eventStartRoom.Invoke();
    }

    public virtual void EndRoom()
    {
        eventStartRoom.Invoke();
    }
}
