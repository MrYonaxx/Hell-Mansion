using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_chargeState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    public ChargeState(Entity entity, FSM stateMachine, string animBoolName, D_chargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.checkPlayerInMinRangeAgro();
        isPlayerInMaxAgroRange = entity.checkPlayerInMaxRangeAgro();
        isDetectingLedge = entity.checkLedge();
        isDetectingWall = entity.checkWall();
    }

    public override void enter()
    {
        base.enter();
        this.entity.RotateToFacePlayer();
        entity.setVelocity(stateData.chargeSpeed);
        isChargeTimeOver = false;
    }

    public override void exit()
    {
        base.exit();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();
        if(Time.time >=startTime+stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
        if(isPlayerInMinAgroRange)
        {
            this.entity.RotateToFacePlayer();
        }
    }
}
