using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;
    public StunState(Entity entity, FSM stateMachine, string animBoolName,D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entity.checkGround();
        performCloseRangeAction = entity.checkPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.checkPlayerInMinRangeAgro();
    }

    public override void enter()
    {
        base.enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        //TODO : petit knockback
        //entity.SetVelocity(stateData.stunKnockBackSpeed, stateData.stunKnockBackAngle, entity.lastDamageDirection); 
        entity.setVelocity(0f);

    }

    public override void exit()
    {
        base.exit();
        entity.resetStunResistance();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();
        if(Time.time>=startTime+stateData.stunTime)
        {
            this.isStunTimeOver = true;
        }
        if(isGrounded && Time.time>startTime+stateData.stunKnockBackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entity.setVelocity(0f);
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
