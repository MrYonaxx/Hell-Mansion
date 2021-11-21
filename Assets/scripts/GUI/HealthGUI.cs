using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    public Slider slider;
    
    // Start is called before the first frame update
   
    public void setHealth(float health)
    {
        slider.value = health;
    }
    public void setMaxHealthPoints(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
