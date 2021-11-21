using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject ennemyPrefab;
    public bool stopSpawning=false;
    public float spawnTime;
    public float spawnDelay;

    public void spawnEnnemy()
    {

        Instantiate(ennemyPrefab, transform.position, ennemyPrefab.transform.rotation);
        if(stopSpawning)
        {
            CancelInvoke("spawnEnnemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerControl>())
        {
            InvokeRepeating("spawnEnnemy", spawnTime, spawnDelay);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControl>())
        {
            stopSpawning = true;
        }
    }
}
