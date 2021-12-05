using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_playerDetectedState : PlayerDetectedState
{
    private Ennemy3 ennemy;
    public E3_playerDetectedState(Entity entity, FSM stateMachine, string animBoolName, D_PlayerDetected stateData, Ennemy3 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (performCloseRangeAction)
        {
            stateMachine.changeState(ennemy.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.changeState(ennemy.rangedAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.changeState(ennemy.lookForPlayerState);
        }

    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }

    // Start is called before the first frame update
}

