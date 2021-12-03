using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeleeAttackState : MeleeAttackState
{
    private Ennemy2 ennemy;
    public E2_MeleeAttackState(Entity entity, FSM stateMachine, string animBoolName, Transform attackPosition, D_meleeAttackState stateDate,Ennemy2 ennemy) : base(entity, stateMachine, animBoolName, attackPosition, stateDate)
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
            //TODO : si ennemie explosif, faut qu'il meurt ici ou on change en explosion state le meleeAttackState (mais explosionState reprend la majorité du code de meleeAttackState)
            stateMachine.changeState(ennemy.deathState);
            /*
            if(isPlayerInMinAgroRange)
            {
                stateMachine.changeState(ennemy.playerDetectedState);
            } else
            {
                stateMachine.changeState(ennemy.lookForPlayerState);
            }
            */
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
