using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScreenManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Load New Scene");
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Has Closed");
    }

    
}
