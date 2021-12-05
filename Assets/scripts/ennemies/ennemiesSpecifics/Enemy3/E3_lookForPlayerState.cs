using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_lookForPlayerState : LookForPlayerState
{
    private Ennemy3 ennemy;
    public E3_lookForPlayerState(Entity entity, FSM stateMachine, string animBoolName, D_LookForPlayerState stateData, Ennemy3 ennemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.ennemy = ennemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        if (isPlayerinMinAgroRange)
        {
            stateMachine.changeState(ennemy.playerDetectedState);
        }
        else if (isAllturnsTimeDone)
        {
            stateMachine.changeState(ennemy.moveState);
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
