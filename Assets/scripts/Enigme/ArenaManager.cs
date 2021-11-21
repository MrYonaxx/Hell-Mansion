using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaManager : MonoBehaviour
{
    [SerializeField]
    List<Entity> ennemies;
    [SerializeField]
    UnityEvent eventEndArena;

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
            eventEndArena.Invoke();
    }
}
