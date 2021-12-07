using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    PlayerControl player;
    [SerializeField]
    Transform neckBone;


    List<IInteractable> interactables = new List<IInteractable>();
    IInteractable objInteractable = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pick") && objInteractable != null)
        {
            objInteractable.Interact(player);

            if (objInteractable.OnlyOnce())
            {
                objInteractable.CanInteract(false);
                interactables.Remove(objInteractable);
                CheckShortestInteractable();
            }
        }
    }


    // On check l'interactabe le plus proche du joueur et on le set en temps qu'objInteractable
    void CheckShortestInteractable()
    {
        if (objInteractable != null)
            objInteractable.CanInteract(false);

        if (interactables.Count == 0)
        {
            objInteractable = null;
            return;
        }
        else if (interactables.Count == 1)
        {
            objInteractable = interactables[0];
        }
        else
        {
            float distanceMin = 9999;
            float distance = 0;
            for (int i = 0; i < interactables.Count; i++)
            {
                distance = (this.transform.position - interactables[i].GetPos()).magnitude;
                if(distance < distanceMin)
                {
                    distance = distanceMin;
                    objInteractable = interactables[i];
                }
            }
        }
        objInteractable.CanInteract(true);
    }

    float tRotation = 1;
    Quaternion previousRotation;

    private void LateUpdate()
    {

        if (neckBone && objInteractable != null)
        {
            Quaternion originRot = neckBone.transform.rotation;
            float z = neckBone.eulerAngles.z;
            neckBone.transform.LookAt(objInteractable.GetPos());
            neckBone.transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);
            neckBone.eulerAngles = new Vector3(neckBone.eulerAngles.x, neckBone.eulerAngles.y, z);



            previousRotation = neckBone.transform.rotation;
            if(tRotation > 0)
                tRotation -= Time.deltaTime * 6;

            neckBone.transform.rotation = Quaternion.Slerp(neckBone.transform.rotation, originRot, tRotation);
            /*Quaternion rot = Quaternion.LookRotation(objInteractable.GetPos() - neckBone.position);
            Quaternion change = Quaternion.Inverse(rot) * previousRotation;
            neckBone.transform.rotation *= change;
            previousRotation = neckBone.rotation;
            return;*/
        }
        else if (tRotation < 1)
        {
            tRotation += Time.deltaTime * 6;
            neckBone.transform.rotation = Quaternion.Slerp(previousRotation, neckBone.transform.rotation, tRotation);
        }
        /*float y = neckBone.eulerAngles.y;
        float z = neckBone.eulerAngles.z;
        neckBone.transform.LookAt(test.position, Vector3.forward);
        neckBone.eulerAngles = new Vector3(neckBone.eulerAngles.x, y, z);
        return;

        if (neckBone && objInteractable != null)
        {
            float angle = Vector3.SignedAngle(this.transform.forward, neckBone.position - objInteractable.GetPos(), Vector3.up);
            Debug.Log("Allo");
            //Quaternion quaterion = Quaternion.RotateTowards
            neckBone.transform.LookAt(objInteractable.GetPos());
            if (Mathf.Abs(angle) < 45)
            {
                Debug.Log("Allo45");
                neckBone.transform.LookAt(objInteractable.GetPos());
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactables.Add(interactable);
            CheckShortestInteractable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if(interactables.Contains(interactable))
            {
                interactable.CanInteract(false);
                interactables.Remove(interactable);
                CheckShortestInteractable();
            }
        }
    }
}
