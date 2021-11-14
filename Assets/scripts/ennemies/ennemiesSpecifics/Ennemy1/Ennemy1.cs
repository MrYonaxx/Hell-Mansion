using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy1 : Entity
{
    public E1_idleState idleState { get; private set; }
    public E1_moveState moveState { get; private set; }
    public E1_playerDetectedState playerDetectedState { get; private set; }
    public E1_chargeState chargeState { get; private set; }
    public E1_lookForPlayerState lookForPlayerState { get; private set; }

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
    public override void Start()
    {
        base.Start();
        //doit être le même nom dans l'animator
        moveState = new E1_moveState(this, stateMachine, "Move", moveStateData, this);
        idleState = new E1_idleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_playerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new E1_chargeState(this, stateMachine, "charge",chargeStateData,this);
        lookForPlayerState = new E1_lookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);
        stateMachine.initialize(moveState);
    }
}
