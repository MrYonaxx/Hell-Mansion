using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailShoot : MonoBehaviour
{

    [SerializeField]
    PlayerControl playerControl = null;

    [SerializeField]
    TrailRenderer trailRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        playerControl.OnShoot += TrailDraw;
    }

    private void OnDestroy()
    {
        playerControl.OnShoot -= TrailDraw;
    }

    void TrailDraw(Vector3 finalPos)
    {

        trailRenderer.Clear();
        trailRenderer.transform.position = playerControl.GetComponentInChildren<GunSystem>().muzzlePosition.transform.position;
        finalPos.y = transform.position.y;
        trailRenderer.Clear();

        if (finalPos == Vector3.zero)
            finalPos = trailRenderer.transform.position + (playerControl.transform.forward * 4);

        StopAllCoroutines();
        StartCoroutine(TrailDrawcoroutine(trailRenderer.transform.position, finalPos, 5f));
    }

    IEnumerator TrailDrawcoroutine(Vector3 startPos, Vector3 endpos, float size)
    {
        for (int i = 1; i < size; i++)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPos, endpos, i / size);// AddPosition(Vector3.Lerp(endpos, startPos, i / size));
            yield return null;
        }
    } 


}
