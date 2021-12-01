using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRota : MonoBehaviour
{
    [SerializeField]
    private Camera main_cam;

    private void Start()
    {
        if (main_cam == null)
            main_cam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + main_cam.transform.rotation * Vector3.forward, main_cam.transform.rotation * Vector3.up);
    }
}
