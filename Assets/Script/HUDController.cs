using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HUDController : MonoBehaviour
{
    public GameObject MessagePanel;
	public GameObject MessagePanelReload;
    public GameObject reloadGameObject;
    public Slider reloadSlider;
    public Text TextControl;

    private void Awake()
    {
	    // Debug.Log(GetComponentInChildren<TextController>());
    }

    public void OpenMessagePanel()
    {
	    MessagePanel.SetActive(true);
	    //set text when we will custom 
    }
    public void CloseMessagePanel()
    {
		MessagePanel.SetActive(false);
    }
	public void OpenMessagePanelReload()
    {
	    MessagePanelReload.SetActive(true);
	    //set text when we will custom 
    }
	
    public void CloseMessagePanelReload()
    {
		MessagePanelReload.SetActive(false);
    }

    public void UpdatePanelBullet(GunSystem EquipGun)
    {
	    if (EquipGun.infiniteAmmo ){
		    TextControl.text = "\u221E";
		    TextControl.fontSize = 40;
	    }
	    else{
		    TextControl.text = EquipGun.bulletLeft + " / " + EquipGun.AmmoReserve ;  
		    TextControl.fontSize = 26;
	    }
    }
    
    public IEnumerator StartReload(float reloadtime)
    {
	    //Debug.Log("anim reload");
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
