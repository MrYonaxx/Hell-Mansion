using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_deathState : DeathState
{
    private Boss1 ennemy;
    public B1_deathState(Entity entity, FSM stateMachine, string animBoolName, D_deathState stateData, Boss1 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
