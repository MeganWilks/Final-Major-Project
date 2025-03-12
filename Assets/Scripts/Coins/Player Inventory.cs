using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCoinInventory : MonoBehaviour
{
    //Coin Settings
    [SerializeField] public int numOfCoin { get; private set; }

    [Header("Events")]
    [SerializeField] public UnityEvent<PlayerCoinInventory> OnCoinsCollected;

    public void CoinsCollected()
    {
        numOfCoin++;
        OnCoinsCollected.Invoke(this);

    }
    
}
