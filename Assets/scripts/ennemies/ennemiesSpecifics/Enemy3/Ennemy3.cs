using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy3 : Entity
{
    public E3_idleState idleState { get; private set; }
    public E3_moveState moveState { get; private set; }
    public E3_playerDetectedState playerDetectedState { get; private set; }
    public E3_lookForPlayerState lookForPlayerState { get; private set; }

    public E3_stunState stunState { get; private set; }
    public E3_deathState deathState { get; private set; }
    public E3_rangedAttackState rangedAttackState { get; private set; }

    public E3_meleeAttackState meleeAttackState { get; private set; }
    [SerializeField]
    private D_idleState idleStateData;
    [SerializeField]
    private D_moveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_meleeAttackState meleeAttackStateDate;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_deathState deathStateData;
    [SerializeField]
    private D_rangedAttackState rangeAttackStateData;
    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangeAttackPosition;
    public override void Start()
    {
        base.Start();
        //doit être le même nom dans l'animator
        moveState = new E3_moveState(this, stateMachine, "Move", moveStateData, this);
        idleState = new E3_idleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E3_playerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new E3_lookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        meleeAttackState = new E3_meleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateDate, this);
        stunState = new E3_stunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new E3_deathState(this, stateMachine, "dead", deathStateData, this);
        rangedAttackState= new E3_rangedAttackState(this, stateMachine, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);
        stateMachine.initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.changeState(deathState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.changeState(stunState);
        } else if(checkPlayerInMinRangeAgro())
        {
            stateMachine.changeState(rangedAttackState);
        }

    }
}
