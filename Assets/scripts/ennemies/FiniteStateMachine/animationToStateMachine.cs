using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationToStateMachine : MonoBehaviour
{
    //this class is use to connect the ennemy1 script with the animator in the alive gameObject
    public AttackState attackState;
    private void triggerAttack()
    {
        attackState.triggerAttack();
    }

    private void finishAttack()
    {
        attackState.finishAttack();
    }
}
