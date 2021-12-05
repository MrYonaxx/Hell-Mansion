using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_moveState : MoveState
{
    private Ennemy3 ennemy;
    public E3_moveState(Entity entity, FSM stateMachine, string animBoolName, D_moveState stateData, Ennemy3 ennemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.ennemy = ennemy;
    }

    public override void enter()
    {
        base.enter();
    }

    public override void exit()
    {
        base.exit();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();

        if (isPlayerInMinAgroRange)
        {
            //Debug.Log("playerDetectedState");
            stateMachine.changeState(ennemy.playerDetectedState);
        }
        else if ((isDetectingWall)) //(isDetectingWall || !isDetectingLedge)
        {
            ennemy.idleState.setFLipAfterIdle(true);
            stateMachine.changeState(ennemy.idleState);
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
