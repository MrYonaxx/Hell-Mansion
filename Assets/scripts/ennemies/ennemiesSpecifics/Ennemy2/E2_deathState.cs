using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_deathState : DeathState
{
    private Ennemy2 ennemy;
    public E2_deathState(Entity entity, FSM stateMachine, string animBoolName, D_deathState stateData,Ennemy2 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
