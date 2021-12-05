using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy4: Entity
{

    public E4_idleState idleState { get; private set; }
    public E4_moveState moveState { get; private set; }
    public E4_playerDetectedState playerDetectedState { get; private set; }
    public E4_chargeState chargeState { get; private set; }
    public E4_lookForPlayerState lookForPlayerState { get; private set; }

    public E4_stunState stunState { get; private set; }
    public E4_deathState deathState { get; private set; }
    public E4_ExplosionState explosionState { get; private set; }

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
        moveState = new E4_moveState(this, stateMachine, "Move", moveStateData, this);
        idleState = new E4_idleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E4_playerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new E4_chargeState(this, stateMachine, "charge",chargeStateData,this);
        lookForPlayerState = new E4_lookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        stunState = new E4_stunState(this, stateMachine, "stun", stunStateData, this);
        deathState = new E4_deathState(this, stateMachine, "dead", deathStateData, this);
        explosionState = new E4_ExplosionState(this, stateMachine, "explosion",meleeAttackPosition, explosionStateData, this);
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
