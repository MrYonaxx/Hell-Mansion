using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
    [SerializeField]
    bool open = false;
    [SerializeField]
    GameObject doorOpen = null;

    Collider interactionCollider = null;


    // Start is called before the first frame update
    void Start()
    {
        interactionCollider = GetComponent<Collider>();
        OpenDoor(open);
    }

    public void OpenDoor()
    {
        open = !open;
        OpenDoor(open);
    }

    public void OpenDoor(bool b)
    {
        doorOpen.SetActive(b);
        interactionCollider.enabled = b;
        open = b;
    }
}
