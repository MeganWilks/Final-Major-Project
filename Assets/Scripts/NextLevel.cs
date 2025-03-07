using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [Header("Game Manager Script")]
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
       gameManager.UnLoadCurrentRoom();
       gameManager.LoadNextRoom();

        
    }
}
