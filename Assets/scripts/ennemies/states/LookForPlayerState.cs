using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{

    protected D_LookForPlayerState stateData;
    protected bool turnImmediately;
    protected bool isPlayerinMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllturnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;
    public LookForPlayerState(Entity entity, FSM stateMachine, string animBoolName,D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

public override void DoChecks()
    {
        base.DoChecks();

        isPlayerinMinAgroRange = entity.checkPlayerInMinRangeAgro();
    }

    public override void enter()
    {
        base.enter();
        isAllTurnsDone = false;
        isAllturnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        entity.setVelocity(0f);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();
        if(turnImmediately)
        {
            entity.flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if(Time.time>=lastTurnTime+stateData.timeBetweenTurns &&  !isAllTurnsDone)
        {
            entity.flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }
        if(amountOfTurnsDone >=stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }
        if(Time.time>=lastTurnTime+stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllturnsTimeDone = true;
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }

    public void setTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
