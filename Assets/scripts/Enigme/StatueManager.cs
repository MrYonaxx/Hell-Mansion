using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueManager : MonoBehaviour
{
    [SerializeField]
    Entity[] entities;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < entities.Length; i++)
        {
            //entities[i].enabled = false;
            entities[i].transform.Find("Alive").gameObject.GetComponent<Animator>().speed = 0;
            entities[i].healthBar.gameObject.SetActive(false);
        }
    }

    public void EnabledEnnemies()
    {
        for (int i = 0; i < entities.Length; i++)
        {
            entities[i].enabled = true;
            entities[i].transform.Find("Alive").gameObject.GetComponent<Animator>().speed = 1;
            entities[i].healthBar.gameObject.SetActive(true);
        }
    }
   /* private IEnumerator InitializeCoroutine()
    {
        yield return new WaitForEndOfFrame(); // On attend une frame que entity s'initialise
        for (int i = 0; i < entities.Length; i++)
        {
            entities[i].enabled = false;
            entities[i].anim.speed = 0;
            entities[i].healthBar.gameObject.SetActive(false);
        }
    }*/



}
