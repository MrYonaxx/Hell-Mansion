using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    protected D_deathState stateData;
    public DeathState(Entity entity, FSM stateMachine, string animBoolName,D_deathState stateData) : base(entity, stateMachine, animBoolName)
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
        //TODO : Voir pour instancier les particules
        /*
        GameObject.Instantiate(stateData.deathBloodParticules, entity.aliveGameObject.transform.position, stateData.deathBloodParticules.transform.rotation);
        GameObject.Instantiate(stateData.deathChunckParticules, entity.aliveGameObject.transform.position, stateData.deathChunckParticules.transform.rotation);
        */
        entity.gameObject.SetActive(false);

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
