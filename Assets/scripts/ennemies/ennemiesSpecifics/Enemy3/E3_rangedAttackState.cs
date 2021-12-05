using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_rangedAttackState : RangedAttackState
{
    private Ennemy3 ennemy;
    public E3_rangedAttackState(Entity entity, FSM stateMachine, string animBoolName, Transform attackPosition, D_rangedAttackState stateData,Ennemy3 ennemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.changeState(ennemy.playerDetectedState);
            } else
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
    }
}
