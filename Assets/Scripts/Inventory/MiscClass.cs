using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{

    public override void Use(PlayerController caller)
    {

        base.Use(caller);
        caller.inventoryManager.Remove(this);
        if (itemName == "Health Potion")
        {
            Health.Heal(10);
            
        }
        else
        {
            Debug.Log("Consume Item");
        }
    }
        

        
        
    
    public override MiscClass GetMisc() { return this;}
}
