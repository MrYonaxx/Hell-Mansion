using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM 
{
    public State currentState { get; private set; } 

    public void initialize(State startingState)
    {
        this.currentState = startingState;
        this.currentState.enter();
    }

    public void changeState(State newState)
    {
        this.currentState.exit();
        this.currentState = newState;
        this.currentState.enter();
    }
}
