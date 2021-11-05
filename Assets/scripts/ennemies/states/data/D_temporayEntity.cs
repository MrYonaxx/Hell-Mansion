using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEntityData",menuName ="Data/Entity Data/Base Data")]
public class D_temporayEntity : ScriptableObject
{
    public float wallCheckDistance=0.2f; //a set dans l'inspecteur
    public float ledgeCheckDistance=0.4f;

    public LayerMask whatisground;
}
