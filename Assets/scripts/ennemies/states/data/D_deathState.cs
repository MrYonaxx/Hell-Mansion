using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeathStateData", menuName = "Data/State Data/Death State")]
public class D_deathState : ScriptableObject
{
    public GameObject deathChunckParticules;
    public GameObject deathBloodParticules;
    [SerializeField]
    public DropStruct[] drops;
}
