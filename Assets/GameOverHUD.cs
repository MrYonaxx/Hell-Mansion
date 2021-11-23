using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHUD : MonoBehaviour
{
	
    public GameObject GameOver;
    
    public void OnDeath()
	{
		GameOver.SetActive(true);

	}

    public void Restart()
    { 
        GameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        GameOver.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
