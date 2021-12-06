using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationToStateMachine : MonoBehaviour
{
    //this class is use to connect the ennemy1 script with the animator in the alive gameObject
    public AttackState attackState;
    public ParticleSystem particleSystemTrigger;
    public ParticleSystem particleSystemExplosion;


    private void triggerAttack()
    {
        particleSystemTrigger.Play();

    }

    private void finishAttack()
    {
        particleSystemExplosion.transform.SetParent(null);
        particleSystemExplosion.Play();
        attackState.triggerAttack();
        attackState.finishAttack();
    }
}
