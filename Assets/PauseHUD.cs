using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHUD : MonoBehaviour
{

    public GameObject Pause;
    public GameObject OptionsMenu;
    PlayerControl player;

    public void OnPause(PlayerControl p)
	{
        player = p;
        player.CanInputPlayer(false);
        OptionsMenu.SetActive(false);
		Pause.SetActive(true);
        Time.timeScale = 0;

	}

    public void ResumeButton()
    {
        player.CanInputPlayer(true);
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
