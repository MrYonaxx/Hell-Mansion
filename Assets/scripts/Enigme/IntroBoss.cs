using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBoss : MonoBehaviour
{

    [SerializeField]
    Entity enemy = null;
    [SerializeField]
    AnimationClip animationClip = null;
    [SerializeField]
    Transform focus = null;

    private void Start()
    {
        Audio.AudioManager.Instance?.StopMusic(5);
    }

    public void IntroBossAnimation()
    {
        StartCoroutine(IntroBossCoroutine());
    }
    private IEnumerator IntroBossCoroutine()
    {

        PlayerControl playerControl = FindObjectOfType<PlayerControl>();
        CameraController cameraController = FindObjectOfType<CameraController>();
        cameraController.AddTarget(focus, 1);
        playerControl.CanInputPlayer(false);

        cameraController.GetComponent<Animator>().Play(animationClip.name);
        enemy.GetComponentInChildren<Rigidbody>().isKinematic = true;
        enemy.gameObject.SetActive(true);
        enemy.healthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        cameraController.GetComponentInChildren<Feedbacks.Shake>().ShakeEffect(0.05f, 180);
        yield return new WaitForSeconds(3f);
        cameraController.GetComponentInChildren<Feedbacks.Shake>().ShakeEffect(1f, 20);
        yield return new WaitForSeconds(2f);

        cameraController.RemoveTarget(focus);
        cameraController.AddTarget(enemy.transform.Find("Alive"), 0);
        yield return new WaitForSeconds(1f);
        playerControl.CanInputPlayer(true);
        enemy.enabled = true;
        enemy.GetComponentInChildren<Rigidbody>().isKinematic = false;
        enemy.healthBar.gameObject.SetActive(true);
    }
}
