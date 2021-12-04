using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_stunState : StunState
{
    private Ennemy4 ennemy;
    public E4_stunState(Entity entity, FSM stateMachine, string animBoolName, D_StunState stateData,Ennemy4 ennemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.changeState(ennemy.explosionState);
            } else if( isPlayerInMinAgroRange)
            {
                stateMachine.changeState(ennemy.chargeState);
            } else
            {
                ennemy.lookForPlayerState.setTurnImmediately(true);
                stateMachine.changeState(ennemy.lookForPlayerState);
            }
        }
    }

    public override void physicsUpdate()
    {
        base.physicsUpdate();
    }
}
