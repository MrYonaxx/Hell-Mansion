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
        int randomIndex = Random.RandomRange(0, stateData.drops.Length);

        float randomTaux = Random.Range(0, 100);
        Debug.Log("randomTaux");
        Debug.Log(randomTaux);
        foreach (DropStruct dropStruct in stateData.drops)
        {
            if(randomTaux<dropStruct.dropChance)
            {
                GameObject.Instantiate(dropStruct.drop, entity.aliveGameObject.transform.position, dropStruct.drop.transform.rotation);
                return;
            }
        }
        
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
