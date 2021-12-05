using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_deathState : DeathState
{
    private Ennemy3 ennemy;
    public E3_deathState(Entity entity, FSM stateMachine, string animBoolName, D_deathState stateData, Ennemy3 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
