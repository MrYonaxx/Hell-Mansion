using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    public Slider slider;
    
    // Start is called before the first frame update
   
    public void setHealth(int health)
    {
        slider.value = health;
    }
    public void setMaxHealthPoints(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
