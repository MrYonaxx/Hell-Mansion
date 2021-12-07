using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public enum WeaponType
    {
        Gun,
        Rifle,
        Rifle3,
        Shotgun,
        smg,
        none
    }
public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public WeaponType Typeweapon;
    public bool pickingItem;
    public PlayerControl playerTrigger;
    [SerializeField] private Rigidbody ElementToRotate;

    [SerializeField]
    private UnityEvent events;
    private void Update()
    {
        if (pickingItem && Input.GetButton("Pick"))
        {
            playerTrigger.startNoWeapon = false;
            switch (Typeweapon)
            {
                case WeaponType.Gun:
                    playerTrigger.SetArsenal(playerTrigger.arsenal[0]);
                    break;
                case WeaponType.Rifle:
                    playerTrigger.SetArsenal(playerTrigger.arsenal[1]);
                    break;
                case WeaponType.Rifle3:
                    playerTrigger.SetArsenal(playerTrigger.arsenal[2]);
                    break;
                case WeaponType.Shotgun:
                    playerTrigger.SetArsenal(playerTrigger.arsenal[3]);
                    break;
                case WeaponType.smg:
                    playerTrigger.SetArsenal(playerTrigger.arsenal[4]);
                    break;
                
            }
            if (playerTrigger != null)
            {
                 playerTrigger.hud.CloseMessagePanel();
            }

            events.Invoke();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        RotateElement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControl>())
        {
            playerTrigger = other.GetComponent<PlayerControl>();
                        
            if (playerTrigger != null)
            {
                playerTrigger.hud.OpenMessagePanel();
                pickingItem = true;
            };
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControl>())
        {
            playerTrigger = other.GetComponent<PlayerControl>();

            if (playerTrigger != null)
            {
                pickingItem = false;
                playerTrigger.hud.CloseMessagePanel();
            }
        }
    }

    private void RotateElement()
    {
        var q = Quaternion.AngleAxis(45, Vector3.up);
        float angle;
        Vector3 axis;
        q.ToAngleAxis(out angle, out axis);

        ElementToRotate.angularVelocity = axis * angle * Mathf.Deg2Rad;



    }

}