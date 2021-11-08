using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
public class D_PlayerDetected : ScriptableObject
{
    //wait actionTime before to do the action after that the player is detected
    public float longRangeActionTime;
}
