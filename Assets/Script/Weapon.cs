using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType
    {
        Hand,
        Gun,
        Rifle
    }
public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private WeaponType Typeweapon;
    void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerControl>();
        if (Player != null)
        {
            Debug.Log(Typeweapon);
            switch (Typeweapon)
            {
                case WeaponType.Hand:
                    Player.SetArsenal("Hand");
                    break;
                case WeaponType.Gun:
                    Player.SetArsenal("Gun");
                    break;
                case WeaponType.Rifle:
                    Player.SetArsenal("Rifle");
                    break;
            }
            
        };

    }
}
