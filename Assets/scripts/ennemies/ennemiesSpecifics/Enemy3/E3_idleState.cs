using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_idleState : IdleState
{
    private Ennemy3 ennemy;
    public E3_idleState(Entity entity, FSM stateMachine, string animBoolName, D_idleState stateData, Ennemy3 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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

