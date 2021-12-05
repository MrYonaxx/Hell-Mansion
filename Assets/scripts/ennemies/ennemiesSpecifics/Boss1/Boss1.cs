using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Entity
{
    public B1_idleState idleState { get; private set; }
    public B1_moveState moveState { get; private set; }
    public B1_playerDetectedState playerDetectedState { get; private set; }
    public B1_lookForPlayerState lookForPlayerState { get; private set; }
    public B1_chargeState chargeState { get; private set; }

public B1_stunState stunState { get; private set; }
    public B1_deathState deathState { get; private set; }
public B1_MeleeAttackState meleeAttackStateFirst { get; private set; }
    public B1_MeleeAttackState meleeAttackStateSecond { get; private set; }
    public B1_MeleeAttackState meleeAttackStateThird { get; private set; }
    [SerializeField]
    private D_chargeState chargeStateData;
    [SerializeField]
    private D_idleState idleStateData;
    [SerializeField]
    private D_moveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_meleeAttackState firstMeleeAttackStateDate;
    [SerializeField]
    private D_meleeAttackState secondMeleeAttackStateDate;
    [SerializeField]
    private D_meleeAttackState thirdMeleeAttackStateDate;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_deathState deathStateData;
    [SerializeField]
    private Transform meleeAttackPosition;
    public override void Start()
    {
        base.Start();
        //doit être le même nom dans l'animator
        moveState = new B1_moveState(this, stateMachine, "Move", moveStateData, this);
        idleState = new B1_idleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new B1_playerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new B1_lookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        meleeAttackStateFirst = new B1_MeleeAttackState(this, stateMachine, "meleeAttack1", meleeAttackPosition, firstMeleeAttackStateDate, this);
        meleeAttackStateSecond = new B1_MeleeAttackState(this, stateMachine, "meleeAttack1", meleeAttackPosition, secondMeleeAttackStateDate, this);
        meleeAttackStateThird = new B1_MeleeAttackState(this, stateMachine, "meleeAttack1", meleeAttackPosition, thirdMeleeAttackStateDate, this);

        stunState = new B1_stunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new B1_deathState(this, stateMachine, "dead", deathStateData, this);
        chargeState = new B1_chargeState(this, stateMachine, "charge", chargeStateData, this);
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
        }
        /*
        else if (checkPlayerInMinRangeAgro())
        {
            stateMachine.changeState(rangedAttackState);
        }
        */

    }
}