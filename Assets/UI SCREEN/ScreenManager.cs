using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScreenManager : MonoBehaviour
{



    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Play Game");
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Has Closed");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Load Main Menu");
    }
    
}
