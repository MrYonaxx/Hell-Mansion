using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_meleeAttackState stateDate;
    protected AttackDetails attackDetails;
    protected bool isPlayerInMinAgroRange;
    public MeleeAttackState(Entity entity, FSM stateMachine, string animBoolName, Transform attackPosition, D_meleeAttackState stateDate) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateDate = stateDate;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void enter()
    {
        base.enter();
        attackDetails.damageAmount = stateDate.attackDamage;
        attackDetails.position = entity.aliveGameObject.transform.position;
        isPlayerInMinAgroRange = entity.checkPlayerInMinRangeAgro();
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
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }

    public override void triggerAttack()
    {
        base.triggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateDate.attackRadius,stateDate.whatIsPlayer);

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage",attackDetails); //TODO : à revoir pour adapter au player de martin
        }
    }
}
