using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKShake : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public float shakeVal = 2;
    public float time = 0.4f;

    float t = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        t = 999999999999999999;
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(t > 0)
        {
            t -= Time.deltaTime;
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, animator.GetBoneTransform(HumanBodyBones.RightHand).position + new Vector3(0, 0.1f, 0));
        }
        if (animator)
        {

            
            /*Vector3 shakeRotation = animator.GetBoneTransform(HumanBodyBones.RightHand).localEulerAngles;
            shakeRotation += new Vector3(UnityEngine.Random.Range(-shakeVal, shakeVal),
                UnityEngine.Random.Range(-shakeVal, shakeVal),
                UnityEngine.Random.Range(-shakeVal, shakeVal));
            animator.Bon(HumanBodyBones.RightHand, Quaternion.Euler(shakeRotation));*/
        }
    }
}
