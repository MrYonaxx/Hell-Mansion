using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaManager : MonoBehaviour
{
    [SerializeField]
    List<Entity> ennemies;
    [SerializeField]
    UnityEvent eventEnemyKilled;
    [SerializeField]
    UnityEvent eventEndArena;

    [SerializeField]
    ParticleSystem particleEndArena;
    [SerializeField]
    GameObject objToFocus = null;

    int killCount = 0;
    int maxKillCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
        maxKillCount = ennemies.Count;

        for (int i = 0; i < ennemies.Count; i++)
        {
            ennemies[i].OnDead += DeathRegister;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < ennemies.Count; i++)
        {
            ennemies[i].OnDead -= DeathRegister;
        }
    }


    void DeathRegister(Entity e)
    {

        ennemies.Remove(e);
        e.OnDead -= DeathRegister;
        killCount += 1;



        if (killCount >= maxKillCount)
        {
            eventEndArena?.Invoke();
            particleEndArena.transform.position = e.aliveGameObject.transform.position;
            particleEndArena.Play();
        }
        else
        {
            eventEnemyKilled?.Invoke();
            Camera.main.GetComponent<Feedbacks.Shake>().ShakeEffect();
        }
    }



    public void EndArena()
    {
        StartCoroutine(EndArenaCoroutine());
    }

    private IEnumerator EndArenaCoroutine()
    {
        Camera.main.GetComponent<Feedbacks.Shake>().ShakeEffect(1f, 1);
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1f);
        float t = 0.2f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            Time.timeScale = t;
            yield return null;
        }
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(0.5f);

        // à ne jamais refaire
        CameraController c = FindObjectOfType<CameraController>();
        if(objToFocus == null)
            objToFocus = FindObjectOfType<Door>().gameObject;
        c.AddTarget(objToFocus.transform, 10);
        yield return new WaitForSecondsRealtime(2f);
        c.RemoveTarget(objToFocus.transform);
    }
}
