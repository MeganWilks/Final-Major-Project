using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    [Header("Scripts")]
    PlayerCoinInventory playerInventory;
    private void OnTriggerEnter(Collider other)
    {

        PlayerCoinInventory playerInventory = other.GetComponent<PlayerCoinInventory>();

        if(playerInventory != null )
        {
            playerInventory.CoinsCollected();
            gameObject.SetActive(false);
        }

        
    }
}
