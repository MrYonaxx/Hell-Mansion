using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Vector3 GetPos();
    void CanInteract(bool b);
    void Interact(PlayerControl player);
}
