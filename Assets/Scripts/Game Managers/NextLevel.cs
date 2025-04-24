using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("Game Manager Script")]
    [SerializeField] GameManager gameManager;
  
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if(other == gameObject.CompareTag("Door"))
        {
            
            gameManager.UnLoadCurrentRoom();
            gameManager.LoadNextRoom();

        }
        if(other.gameObject.CompareTag("FinalDoor"))
        {
            SceneManager.LoadSceneAsync(3);
            
        }
    }

    
}
