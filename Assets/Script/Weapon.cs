using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType
    {
        Gun,
        Rifle
    }
public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public WeaponType Typeweapon;
    private bool pickingItem;
    private PlayerControl playerTrigger;
    [SerializeField] private Rigidbody ElementToRotate;
    private void Update()
    {
        if (pickingItem && Input.GetButton("Pick"))
        {
            switch (Typeweapon)
            {
                case WeaponType.Gun:
                    playerTrigger.SetArsenal("Gun");
                    break;
                case WeaponType.Rifle:
                    playerTrigger.SetArsenal("Rifle");
                    break;
            }
            if (playerTrigger != null)
            {
                 playerTrigger.hud.CloseMessagePanel();
            }
           
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        RotateElement();
    }

    void OnTriggerEnter(Collider other)
    {
        playerTrigger = other.GetComponent<PlayerControl>();
            
        if (playerTrigger != null)
        {
            playerTrigger.hud.OpenMessagePanel("");
            pickingItem = true;
        };
    }

    private void OnTriggerExit(Collider other)
    {
        playerTrigger= other.GetComponent<PlayerControl>();

        if (playerTrigger != null)
        {
            pickingItem = false;
            playerTrigger.hud.CloseMessagePanel();
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