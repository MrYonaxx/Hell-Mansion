using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HUDController : MonoBehaviour
{
    public GameObject MessagePanel;
    public GameObject reloadGameObject;
    public Slider reloadSlider;
    public void OpenMessagePanel(string Text)
    {
	    MessagePanel.SetActive(true);
	    //set text when we will custom 
    }
    public void CloseMessagePanel()
    {
		MessagePanel.SetActive(false);
    }

    public IEnumerator StartReload(float reloadtime)
    {
	    Debug.Log("anim reload");
	    reloadGameObject.SetActive(true);
	    float normalizedTime = 0;

	    while (normalizedTime <= 1f)
	    {
		    reloadSlider.value = normalizedTime;
		    normalizedTime += Time.deltaTime / reloadtime;
		    yield return null;
	    }
	    reloadGameObject.SetActive(false);
    }

}
