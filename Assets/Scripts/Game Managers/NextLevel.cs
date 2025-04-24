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
        if(gameObject.tag == "Door")
        {
            
            gameManager.LoadNewRoom();

        }
        if(gameObject.tag == "FinalDoor")
        {
            if (gameManager.Rooms[gameManager.RoomIndex].keyToRemove == null) return;
            if (gameManager.Rooms[gameManager.RoomIndex].enemiesInRoom != 0) return;
            SceneManager.LoadScene(3);
            
        }
    }

    
}
