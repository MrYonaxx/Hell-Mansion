using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void Graphics(int quality)
    {
        
    }

    public void Volume(float value)
    {
        //int volume = (int)(value * 10);
        AudioManager.Instance.SetMusicVolume(value);
        AudioManager.Instance.SetSoundVolume(value);
    }

    public void play()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void quit()
    {
        Application.Quit();
    }
}
