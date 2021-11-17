using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
    [SerializeField]
    bool open = false;
    [SerializeField]
    GameObject doorRenderer = null;

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
        doorRenderer.SetActive(b);
        interactionCollider.enabled = b;
        open = b;
    }
}
