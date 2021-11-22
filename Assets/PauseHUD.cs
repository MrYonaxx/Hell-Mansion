using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHUD : MonoBehaviour
{

    public GameObject Pause;

    public void OnPause()
	{
		Pause.SetActive(true);
        Time.timeScale = 0;

	}

    public void ResumeButton()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
