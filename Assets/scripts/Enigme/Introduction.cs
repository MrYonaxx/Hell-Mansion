using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    PlayerControl playerControl;

    [SerializeField]
    Animator cameraController;
    [SerializeField]
    AnimationClip clip;

    [SerializeField]
    UnityEvent eventStart;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroCoroutine());
    }

    IEnumerator IntroCoroutine()
    {
        playerControl.CanInputPlayer(false);
        cameraController.Play(clip.name);

        yield return new WaitForSeconds(4f);

        playerControl.CanInputPlayer(true);
        eventStart.Invoke();
    }
}
