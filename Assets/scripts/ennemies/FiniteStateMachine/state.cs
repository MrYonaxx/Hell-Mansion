using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FSM stateMachine;
    protected Entity entity;

    public float startTime { get; protected set; }

    protected string animBoolName;

    public State(Entity entity,FSM stateMachine,string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName,true);
        DoChecks();
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
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
