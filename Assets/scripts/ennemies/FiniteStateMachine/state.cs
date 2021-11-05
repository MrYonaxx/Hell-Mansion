using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FSM stateMachine;
    protected temporaryEntity entity;

    protected float startTime;

    protected string animBoolName;

    public State(temporaryEntity entity,FSM stateMachine,string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName,true);
    }

    public virtual void exit()
    {
        entity.anim.SetBool(animBoolName, false);

    }

    public virtual void logicUpdate()
    {

    }

    public virtual void physicsUpdate()
    {

    }
}
