using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_ExplosionState : ExplosionState
{
 private Ennemy2 ennemy;

    public E2_ExplosionState(Entity entity, FSM stateMachine, string animBoolName, Transform attackPosition, D_explosionState stateDate,Ennemy2 ennemy) : base(entity, stateMachine, animBoolName, attackPosition, stateDate)
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
        Debug.Log("Explosion");
        base.logicUpdate();
        if(isAnimationFinished)
        {
            stateMachine.changeState(ennemy.deathState);
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
