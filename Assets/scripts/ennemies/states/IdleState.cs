using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_idleState stateData;
    protected bool flipAfterIdle;
    protected float idleTime;
    protected bool isIdleTimeOver;
    // Start is called before the first frame update
    public IdleState(Entity entity, FSM stateMachine, string animBoolName,D_idleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData=stateData;
    }

    public override void enter()
    {
        Debug.Log("idle");
        base.enter();
        entity.setVelocity(0f);
        isIdleTimeOver = false;
        setRandomIdleTime();
        Debug.Log(idleTime);
    }

    public override void exit()
    {
        base.exit();
        /*
        if(flipAfterIdle)
        {
            entity.flip();
        }
        */
    }

    public override void logicUpdate()
    {

        base.logicUpdate();
        if(Time.time>=startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }

    public void setFLipAfterIdle(bool doFlip)
    {
        this.flipAfterIdle = doFlip;
    }

    private void setRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime,stateData.maxIdleTime);
    }
}
