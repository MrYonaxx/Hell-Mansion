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

    private PlayerControl player;
    public PlayerControl Player
    {
        get { return player; }
    }


    // Fonction initialisant la salle
    public virtual void StartRoom()
    {
        eventStartRoom.Invoke();
    }

    public virtual void EndRoom()
    {
        eventEndRoom.Invoke();
    }



    // Donne à la salle une référence au player pour que les objets de la room puissent retrouver le player
    public void SetPlayer(PlayerControl newPlayer)
    {
        player = newPlayer;
    }

    public void SetPlayerInput(bool b)
    {
        player.CanInputPlayer = b;
    }
}
