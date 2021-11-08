using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    public D_PlayerDetected stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;

    public PlayerDetectedState(Entity entity, FSM stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.checkPlayerInMinRangeAgro();
        isPlayerInMaxAgroRange = entity.checkPlayerInMaxRangeAgro();
    }

    public override void enter()
    {
        base.enter();
        entity.setVelocity(0f);
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
    }
}
