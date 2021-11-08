using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy1 : Entity
{
    public E1_idleState idleState { get; private set; }
    public E1_moveState moveState { get; private set; }

    [SerializeField]
    private D_idleState idleStateData;
    [SerializeField]
    private D_moveState moveStateData;

    public override void Start()
    {
        base.Start();
        //doit �tre le m�me nom dans l'animator
        moveState = new E1_moveState(this, stateMachine, "Move", moveStateData, this);
        idleState = new E1_idleState(this, stateMachine, "idle", idleStateData, this);
        stateMachine.initialize(idleState);
    }
}
