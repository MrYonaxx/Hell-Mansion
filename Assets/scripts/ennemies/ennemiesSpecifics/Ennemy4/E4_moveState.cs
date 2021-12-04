using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_moveState : MoveState
{
    private Ennemy4 ennemy;
    public E4_moveState(Entity entity, FSM stateMachine, string animBoolName, D_moveState stateData,Ennemy4 ennemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.ennemy = ennemy;
    }

    public override void enter()
    {
        base.enter();
        Debug.Log("je vais bouger");
    }

    public override void exit()
    {
        base.exit();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();

        if(isPlayerInMinAgroRange)
        {
            //Debug.Log("playerDetectedState");
            stateMachine.changeState(ennemy.playerDetectedState);
        }
        else if ((isDetectingWall)) //(isDetectingWall || !isDetectingLedge)
        {
            Debug.Log("changeToIdle");
            ennemy.idleState.setFLipAfterIdle(true);
            stateMachine.changeState(ennemy.idleState);
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
