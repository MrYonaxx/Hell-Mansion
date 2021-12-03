using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newExplosionStateData", menuName = "Data/State Data/Explosion State")]

public class D_explosionState : ScriptableObject
{
    public float attackRadius = 2f;
    public LayerMask whatIsPlayer;
    public float attackDamage = 2f;
}