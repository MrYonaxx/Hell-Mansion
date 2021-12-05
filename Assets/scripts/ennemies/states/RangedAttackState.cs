using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected D_rangedAttackState stateData;
    protected GameObject projectile;
    protected Projectile projectileScript;
    protected bool isPlayerInMinAgroRange;
    public RangedAttackState(Entity entity, FSM stateMachine, string animBoolName, Transform attackPosition,D_rangedAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void enter()
    {
        base.enter();
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
        projectile = GameObject.Instantiate(stateData.projectile,attackPosition.position,attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileVelocity, stateData.projectileTravelDistance, stateData.projectileDamage);
    }
}
