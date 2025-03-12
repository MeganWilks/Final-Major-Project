using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Class
public abstract class Item: MonoBehaviour
{
    // Every Item + Behaviour
    [Header("Item Inventory")]
    public string itemName;
    public Sprite itemIcon;
    public abstract void Use();
    
}

// Bomb Class
public class Bomb: Item
{
    [SerializeField] public int damage;
    public override void Use()
    {
        Debug.Log($"Throwing a bomb that deals {damage} damage!");
    }
}

// Shield Class
public class Shield: Item
{
    [SerializeField] public int blockDamageAmount;
    public override void Use()
    {
        Debug.Log($"Blocking an attack for {blockDamageAmount} HP");
    }
}

//Arrow Class
public class Arrow: Item
{
    [SerializeField] public int arrowDamageAmount;

    public override void Use()
    {
       Debug.Log($"Arrow Dealt {arrowDamageAmount}");
    }
}

//Health Potion Class

public class HealthPotion: Item
{
    [SerializeField] public int healAmount;
    public override void Use()
    {
        Debug.Log($"Restoring {healAmount} health!");
    }
}

//Inventory Class
public class PlayerInventory: MonoBehaviour
{
    public List<Item> items = new List<Item>();
    
    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"{item.itemName} added to inventory.");
    }

    public void UseItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex > items.Count) return;
        items[itemIndex].Use();

        
    }
}



