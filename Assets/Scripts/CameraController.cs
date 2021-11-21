using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;


[System.Serializable]
public class TargetsCamera
{
    public Transform transform;
    public int priority;

    public TargetsCamera(Transform t, int p)
    {
        transform = t;
        priority = p;
    }
}


public class CameraController : MonoBehaviour
{
    [SerializeField]
    List<TargetsCamera> targets = new List<TargetsCamera>();

    [Header("Object Reference")]
    [SerializeField]
    private Camera cam;
    public Camera Camera
    {
        get { return cam; }
    }
    [SerializeField]
    BoxCollider boundsCamera;


    [Header("Parameter")]
    [SerializeField]
    Vector3 offset = Vector3.zero;
    [SerializeField]
    float smoothTime = 0.5f;

    // Utiliser par l'animator (c'est un peu bizarre)
    [SerializeField]
    [HideInInspector]
    float bonusSmoothTime = 0f;



    private Vector3 velocity;
    private Vector3 newPos;

    private bool canFocus = true;


    private Bounds cameraView;
    public Bounds CameraView
    {
        get { return cameraView; }
    }




    private void Start()
    {
        if(cam == null)
            cam = GetComponent<Camera>();
        if (boundsCamera == null)
        {
            boundsCamera = this.gameObject.AddComponent<BoxCollider>();
            boundsCamera.size = new Vector3(99999999, 99999999, 9999999);
            boundsCamera.isTrigger = true;
            //boundsCamera.bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(99999999, 99999999, 9999999));
        }
    }


    private void LateUpdate()
    {
        MoveCamera();
    }


    //Move camera position smoothly by calculate position of all targets
    void MoveCamera()
    {
        //Calculate centerpoint between all targets to have a center for camera
        Bounds targetsBounds = CalculateNewBoundsEncapsulate();
        Vector3 centerPoint = targetsBounds.center;

        //Calculate camera view and resize zoom to fit the bound of camera view
        Bounds bluePlane = CalculateBoundsCameraView(centerPoint);

        // Clamp the camera view
        Bounds d = boundsCamera.bounds;
        float x = 0;
        if (bluePlane.min.x < d.min.x)
            x = d.min.x - bluePlane.min.x;
        else if (bluePlane.max.x > d.max.x)
            x = d.max.x - bluePlane.max.x;

        float y = 0;
        if (bluePlane.min.y < d.min.y)
            y = d.min.y - bluePlane.min.y;
        else if (bluePlane.max.y > d.max.y)
            y = d.max.y - bluePlane.max.y;

        float z = 0;
        if (bluePlane.min.z < d.min.z)
            z = d.min.z - bluePlane.min.z;
        else if (bluePlane.max.z > d.max.z)
            z = d.max.z - bluePlane.max.z;

        //Calculate new Position for the camera by calculating centerpoint with an offset
        newPos = centerPoint + offset;
        newPos.x += x;
        newPos.y += y;
        newPos.z += z;
       // newPos.z = cam.transform.position.z;
       // newPos.z += z;

        cameraView = bluePlane;
        cameraView.center += new Vector3(x, y, z);

        //Change transform position smoothly without jitter from new Pos vector we got.
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, Mathf.Max(0, smoothTime + bonusSmoothTime));
    }


    Bounds CalculateNewBoundsEncapsulate()
    {
        // int c'est plus léger
        List<int> finalTargets = new List<int>();

        // Check highest camera priority
        int bestPriority = -9999;
        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i].priority > bestPriority)
            {
                bestPriority = targets[i].priority;
                finalTargets.Clear();
                finalTargets.Add(i);
            }
            else if (targets[i].priority == bestPriority)
            {
                finalTargets.Add(i);
            }
        }

        if (finalTargets.Count == 0)
            return new Bounds();

        Bounds bounds = new Bounds(targets[finalTargets[0]].transform.position, Vector3.zero);

        //Encapsule all targets in the bounds
        for (int i = 1; i < finalTargets.Count; i++)
        {
            bounds.Encapsulate(targets[finalTargets[i]].transform.position);
        }
        return bounds;

    }

    private Bounds CalculateBoundsCameraView(Vector3 center)
    {
        float frustumHeight = 2f * cam.orthographicSize;
        if (frustumHeight >= boundsCamera.size.y)
        {
            frustumHeight = boundsCamera.size.y;
        }
        float frustumWidth = frustumHeight * cam.aspect;
        return new Bounds(center, new Vector3(frustumWidth, frustumHeight));
    }









    public void AddTarget(Transform t, int priority)
    {
        targets.Add(new TargetsCamera(t, priority));
    }


    public void ModifyTargetPriority(Transform t, int newPriority)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i].transform == t)
            {
                targets[i].priority = newPriority;
                return;
            }
        }
    }

    public void RemoveTarget(Transform t)
    {
        for (int i = targets.Count-1; i >= 0; i--)
        {
            if (targets[i].transform == t)
            {
                targets.RemoveAt(i);
                return;
            }
        }
    }







    // DEBUG

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        if (cam == null)
            cam = GetComponent<Camera>();
        if (boundsCamera == null)
            return;

        Bounds CameraWire = CalculateNewBoundsEncapsulate();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CameraWire.center, CameraWire.size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(newPos, CalculateBoundsCameraView(CameraWire.center).size);
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(CameraWire.center, 0.3f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(boundsCamera.transform.position.x - boundsCamera.center.x, boundsCamera.transform.position.y + boundsCamera.center.y), boundsCamera.size);
        Gizmos.color = Color.magenta;
    }
#endif





}