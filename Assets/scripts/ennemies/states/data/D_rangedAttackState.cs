using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_rangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage=1;
    public float projectileVelocity=12;
    public float projectileTravelDistance;
}
