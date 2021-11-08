using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_moveState : MoveState
{
    private Ennemy1 ennemy;
    public E1_moveState(Entity entity, FSM stateMachine, string animBoolName, D_moveState stateData,Ennemy1 ennemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.ennemy = ennemy;
    }

    public override void enter()
    {
        Debug.Log("enter in move");
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
