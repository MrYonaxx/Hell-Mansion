using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brasero : MonoBehaviour
{ 

    [SerializeField]
    ParticleSystem fire = null;
    [SerializeField]
    Light fireLight = null;

    [Header("Parameters")]
    [SerializeField]
    public bool lit = false;
    [SerializeField]
    float timeLight = 1f;
    [SerializeField]
    float intensityLight = 1f;

    private IEnumerator lightCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        LightBrasero(lit);
    }

    public void LightBrasero()
    {
        lit = !lit;
        LightBrasero(lit);
    }
    public void LightBrasero(bool b)
    {
        if (b)
            BraseroOn();
        else
            BraseroOff();
    }

    void BraseroOn()
    {
        fire.Play();

        if (lightCoroutine != null)
            StopCoroutine(lightCoroutine);
        lightCoroutine = LightCoroutine(timeLight, intensityLight, 0);
        StartCoroutine(lightCoroutine);
    }

    void BraseroOff()
    {
        fire.Stop();

        if (lightCoroutine != null)
            StopCoroutine(lightCoroutine);
        lightCoroutine = LightCoroutine(timeLight, 0, fireLight.intensity);
        StartCoroutine(lightCoroutine);
    }

    IEnumerator LightCoroutine(float time, float targetValue, float startValue)
    {
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / time;
            fireLight.intensity = Mathf.Lerp(startValue, targetValue, t);
            yield return null;
        }
        lightCoroutine = null;
    }
}
