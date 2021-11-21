using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour
{
    // vars to Flash a colour when hit 
    public Color NormalColour = Color.white;
    public Color FlashColour = Color.red;

    public Renderer GameMesh;
    public float FlashDelay = 0.5f;
    public int TimesToFlash = 3;

    public void Flash(float duration){

        StartCoroutine(Coroutinedamage(duration));
    }

    private IEnumerator  Coroutinedamage(float duration) {
        var renderer = GameMesh; 
        if (renderer != null) {  

            for (int i = 1; i <= duration/(FlashDelay * 2); i++) {
                foreach (var mat in renderer.materials)
                {
                    mat.color = FlashColour; 
                }
                yield return new WaitForSeconds (FlashDelay);
                foreach (var mat in renderer.materials)
                {
                    mat.color = NormalColour; 
                }
                yield return new WaitForSeconds (FlashDelay);
            }
        }
    }
}
