using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public TrueChest chest;
   private void triggerOpen()
    {
        chest.triggerOpen();
    }

    private void finishOpen()
    {
        chest.finishOpen();
    }
}
