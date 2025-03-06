using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    [Header("Scripts")]
    PlayerInventory playerInventory;
    private void OnTriggerEnter(Collider other)
    {

        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if(playerInventory != null )
        {
            playerInventory.CoinsCollected();
            gameObject.SetActive(false);
        }

        
    }
}
