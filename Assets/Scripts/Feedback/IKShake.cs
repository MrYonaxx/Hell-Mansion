using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKShake : MonoBehaviour
{

    protected Animator animator;
    public Entity enemy;
    public Transform bone;
    public bool ikActive = false;
    public float shakeVal = 2;
    public float time = 0.4f;

    float t = 0f;

    void Start()
    {
        enemy.OnHit += ShakeDamage;
        animator = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        enemy.OnHit -= ShakeDamage;
    }

    public void ShakeDamage(Entity e)
    {
        ShakeDamage(time);
    }

    public void ShakeDamage(float time)
    {
        t = time;
    }

    //a callback for calculating IK
    void LateUpdate()
    {
        if (t > 0)
        {
            t -= Time.deltaTime;
            bone.transform.position += new Vector3(UnityEngine.Random.Range(-shakeVal, shakeVal),
                                                    UnityEngine.Random.Range(-shakeVal, shakeVal),
                                                    UnityEngine.Random.Range(-shakeVal, shakeVal));
        }
    }
}
