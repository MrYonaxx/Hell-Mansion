using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_idleState : IdleState
{
    private Boss1 ennemy;
    public B1_idleState(Entity entity, FSM stateMachine, string animBoolName, D_idleState stateData, Boss1 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
            stateMachine.changeState(ennemy.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            //setFLipAfterIdle(true);
            stateMachine.changeState(ennemy.moveState);
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
