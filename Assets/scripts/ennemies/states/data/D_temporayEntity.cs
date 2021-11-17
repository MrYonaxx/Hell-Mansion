using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newEntityData",menuName ="Data/Entity Data/Base Data")]
public class D_temporayEntity : ScriptableObject
{
    public float maxHealth = 30f;
    public float damageHopSpeed = 3f; //the velocity knockback
    public string MonsterName="Bouboule";
    public float power=1f; //Coef de difficulté de l'ennemie

    public float wallCheckDistance=0.2f; //a set dans l'inspecteur
    public float ledgeCheckDistance=0.4f;

    public float minAgroDistance = 3f;//use field of view in the editor
    public float maxAgroDistance=4f;

    public float closeRangeActionDistance=1f;

    public LayerMask whatisground;
    public LayerMask whatIsPlayer;
}
