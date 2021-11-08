using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon;
    public string desc = "";
    

    
    public virtual void Use() {
        Debug.Log("Using " + name);
    }
}
