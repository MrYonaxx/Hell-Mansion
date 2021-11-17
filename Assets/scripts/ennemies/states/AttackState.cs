using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    public AttackState(Entity entity, FSM stateMachine, string animBoolName,Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void enter()
    {
        base.enter();
        entity.atsm.attackState = this;
        isAnimationFinished = false;
        entity.setVelocity(0f);
    }

    public override void exit()
    {
        base.exit();
    }

    public override void logicUpdate()
    {
        base.logicUpdate();
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }

    public virtual void triggerAttack() //call when the anim start the attack
    {

    }

    public virtual void finishAttack() //call when anim finished
    {
        isAnimationFinished = true;
    }
}
