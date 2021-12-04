using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKShoot : MonoBehaviour
{
    protected PlayerControl player;
    protected Animator animator;

    public float time = 0.4f;
    public float height = 0.1f;
    float t = 0f;
    Vector3 targetPosRight = Vector3.zero;
    Vector3 targetPosLeft = Vector3.zero;

    void Start()
    {
        player = GetComponent<PlayerControl>();
        animator = GetComponent<Animator>();
        player.OnShoot += ShootFeedback;
    }

    private void OnDestroy()
    {
        player.OnShoot -= ShootFeedback;
    }

    // Redirection pour event;
    public void ShootFeedback(Vector3 pos)
    {
        ShootFeedback(time);
    }

    public void ShootFeedback(float newTime)
    {
        time = newTime;
        t = time;
        targetPosRight = animator.GetBoneTransform(HumanBodyBones.RightHand).position + new Vector3(0, height, 0);
        targetPosLeft = animator.GetBoneTransform(HumanBodyBones.LeftHand).position + new Vector3(0, height, 0);
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(t > 0)
        {
            t -= Time.deltaTime;

            float factor = t / time;
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, factor);
            animator.SetIKPosition(AvatarIKGoal.RightHand, targetPosRight);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, factor);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, targetPosLeft);
        }
    }
}
