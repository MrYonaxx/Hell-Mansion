using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRota : MonoBehaviour
{
    [SerializeField]
    private Camera main_cam;
 
    void Update()
    {
        transform.LookAt(transform.position + main_cam.transform.rotation * Vector3.forward, main_cam.transform.rotation * Vector3.up);
    }
}
