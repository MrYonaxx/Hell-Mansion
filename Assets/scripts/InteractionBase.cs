using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Audio;

[System.Serializable]
public class UnityEventPlayer : UnityEvent<PlayerControl>
{

}

[RequireComponent(typeof(Collider))]
public class InteractionBase : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected GameObject mouseIcon;

    [SerializeField]
    private UnityEventPlayer eventPlayer;

    [SerializeField]
    private bool onlyOnce;

    private PlayerControl previousPlayer = null;

    public Vector3 GetPos()
    {
        return this.transform.position;
    }

    public void CanInteract(bool b)
    {
        mouseIcon.SetActive(b);
    }

    public bool OnlyOnce()
    {
        return onlyOnce;
    }

    public void Interact(PlayerControl player)
    {
        previousPlayer = player;
        eventPlayer.Invoke(player);
    }



    // Fonctions à utilisés pour les Events

    public void StopInputPlayer(PlayerControl player)
    {
        player.CanInputPlayer(false);
    }

    public void StartInputPlayer(PlayerControl player)
    {
        player.CanInputPlayer(true);
    }


    // Utilise le player enregistré dans Interact()
    public void StopInputPlayer()
    {
        previousPlayer.CanInputPlayer(false);
    }
    public void StartInputPlayer()
    {
        previousPlayer.CanInputPlayer(true);
    }



    public void PlaySound(AudioClip clip)
    {
        AudioManager.Instance.PlaySound(clip, 1);
    }
}
