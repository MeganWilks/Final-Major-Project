using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coinText;
   
    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText(PlayerCoinInventory playerInventory)
    {
        coinText.text = playerInventory.numOfCoin.ToString();

    }
}
