using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_moveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;

    public MoveState(Entity entity, FSM stateMachine, string animBoolName,D_moveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = entity.checkLedge();
        isDetectingWall = entity.checkWall();
        isPlayerInMinAgroRange = entity.checkPlayerInMinRangeAgro();
    }

    public override void enter()
    {
        base.enter();
        Debug.Log("enter move");

        //get a random position and walk to it
        entity.setVelocity(stateData.movementSpeed);

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
    }

    // Start is called before the first frame update
}
