using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy2 : Entity
{

    public E2_idleState idleState { get; private set; }
    public E2_moveState moveState { get; private set; }
    public E2_playerDetectedState playerDetectedState { get; private set; }
    public E2_chargeState chargeState { get; private set; }
    public E2_lookForPlayerState lookForPlayerState { get; private set; }

    public E2_stunState stunState { get; private set; }
    public E2_deathState deathState { get; private set; }
    public E2_ExplosionState explosionState { get; private set; }

    public E2_MeleeAttackState meleeAttackState { get; private set; }
    [SerializeField]
    private D_idleState idleStateData;
    [SerializeField]
    private D_moveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_chargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_meleeAttackState meleeAttackStateDate;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_deathState deathStateData;
    [SerializeField]
    private D_explosionState explosionStateData;
    [SerializeField]
    private Transform meleeAttackPosition;

    [SerializeField]
    private AttackDetails mort;
    public override void Start()
    {
        base.Start();
        //doit être le même nom dans l'animator
        moveState = new E2_moveState(this, stateMachine, "Move", moveStateData, this);
        idleState = new E2_idleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_playerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new E2_chargeState(this, stateMachine, "charge",chargeStateData,this);
        lookForPlayerState = new E2_lookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateDate, this);
        stunState = new E2_stunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new E2_deathState(this, stateMachine, "dead", deathStateData, this);
        explosionState = new E2_ExplosionState(this, stateMachine, "explosion",meleeAttackPosition, explosionStateData, this);
        stateMachine.initialize(moveState);
    }
    public override void Update()
    {
        base.Update();
        Debug.Log(stateMachine.currentState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.changeState(deathState);
        }
        else if (isStunned && stateMachine.currentState!=stunState)
        {
            stateMachine.changeState(stunState);
        }

    }
}
