using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_playerDetectedState : PlayerDetectedState
{
    private Boss1 ennemy;
    public B1_playerDetectedState(Entity entity, FSM stateMachine, string animBoolName, D_PlayerDetected stateData, Boss1 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
            switch (this.ennemy.numberOfAttacks)
            {
                case 0:
                    stateMachine.changeState(ennemy.meleeAttackStateFirst);
                    break;
                case 1:
                    stateMachine.changeState(ennemy.meleeAttackStateSecond);
                    break;
                case 2:
                    stateMachine.changeState(ennemy.meleeAttackStateThird);
                    break;
            }
        }
        else if (performLongRangeAction)
        {
            stateMachine.changeState(ennemy.chargeState);
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
