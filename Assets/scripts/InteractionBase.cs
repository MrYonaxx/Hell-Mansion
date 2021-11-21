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
        eventPlayer.Invoke(player);
    }

    public void StopInputPlayer(PlayerControl player)
    {
        player.CanInputPlayer(false);
    }

    public void StartInputPlayer(PlayerControl player)
    {
        player.CanInputPlayer(true);
    }

    public void PlaySound(AudioClip clip)
    {
        AudioManager.Instance.PlaySound(clip, 1);
    }
}
