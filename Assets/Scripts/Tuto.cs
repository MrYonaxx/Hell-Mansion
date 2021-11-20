using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{

    [SerializeField]
    protected GameObject Interaction;

    public void dialogue1()
    {
        Interaction.SetActive(false);
    }
    
}
