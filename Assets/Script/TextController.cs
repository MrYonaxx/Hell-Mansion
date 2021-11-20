using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextController : MonoBehaviour
{
    public Text TextBox;
    private string infini = "\u221E";

    public void UpdateText(int bulletLeft, int AmmoReserve){
        if (AmmoReserve != -1){
            TextBox.text = bulletLeft + " / " + AmmoReserve ;  
            TextBox.fontSize = 26;
        }
        else{
            TextBox.text = infini;
            TextBox.fontSize = 40;  
        }
    }

}
