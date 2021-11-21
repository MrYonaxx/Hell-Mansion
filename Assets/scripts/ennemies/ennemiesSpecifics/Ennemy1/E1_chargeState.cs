using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_chargeState : ChargeState
{
    private Ennemy1 ennemy;
    public E1_chargeState(Entity entity, FSM stateMachine, string animBoolName, D_chargeState stateData, Ennemy1 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (performCloseRangeAction)
        {
            stateMachine.changeState(ennemy.meleeAttackState);
        }
        else if (isDetectingWall)
        {
            stateMachine.changeState(ennemy.lookForPlayerState);
        }
        else if(isChargeTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.changeState(ennemy.playerDetectedState);
            } else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.changeState(ennemy.lookForPlayerState);
            }
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
