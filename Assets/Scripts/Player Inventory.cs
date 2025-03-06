using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Coin Settings
    [SerializeField] public int numOfCoin { get; private set; }

    public void CoinsCollected()
    {
        numOfCoin++;

    }
    
}
