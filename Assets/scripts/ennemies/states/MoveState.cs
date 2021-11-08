using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_moveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    public MoveState(Entity entity, FSM stateMachine, string animBoolName,D_moveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void enter()
    {
        Debug.Log("Move");
        base.enter();
        //get a random position and walk to it
        entity.setVelocity(stateData.movementSpeed);
        isDetectingLedge = entity.checkLedge();
        isDetectingWall = entity.checkWall();
    }

    public override void exit()
    {
        base.exit();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
        isDetectingLedge = entity.checkLedge();
        isDetectingWall = entity.checkWall();
    }

    // Start is called before the first frame update
}
