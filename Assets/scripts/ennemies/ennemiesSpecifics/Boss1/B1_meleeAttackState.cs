using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_MeleeAttackState : MeleeAttackState
{
    private Boss1 ennemy;
    public B1_MeleeAttackState(Entity entity, FSM stateMachine, string animBoolName, Transform attackPosition, D_meleeAttackState stateDate, Boss1 ennemy) : base(entity, stateMachine, animBoolName, attackPosition, stateDate)
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

    public override void finishAttack()
    {
        base.finishAttack();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.changeState(ennemy.playerDetectedState);
            }
            else
            {
                stateMachine.changeState(ennemy.lookForPlayerState);
            }
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }

    public override void triggerAttack()
    {
        base.triggerAttack();
        this.ennemy.numberOfAttacks++;
        if(this.ennemy.numberOfAttacks>=3)
        {
            this.ennemy.numberOfAttacks = 0;
        }
    }
}
