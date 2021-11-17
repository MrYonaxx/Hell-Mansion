using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    public D_PlayerDetected stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;

    public PlayerDetectedState(Entity entity, FSM stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.checkPlayerInMinRangeAgro();
        isPlayerInMaxAgroRange = entity.checkPlayerInMaxRangeAgro();
        performCloseRangeAction = entity.checkPlayerInCloseRangeAction();
    }

    public override void enter()
    {
        base.enter();
        entity.setVelocity(0f);
        //orienter le forward vers le joueur
        entity.RotateToFacePlayer();
        performLongRangeAction = false;

    }

    public override void exit()
    {
        base.exit();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();
        if(Time.time>=startTime+stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
        entity.RotateToFacePlayer();
    }
}
