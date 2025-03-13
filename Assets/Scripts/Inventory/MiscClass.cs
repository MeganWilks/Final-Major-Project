using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{
    [Header("Misc")]
    [SerializeField] private int HealthAdded;


    public override void Use(PlayerController caller)
    {
        base.Use(caller);
        Debug.Log("Consume Item");
        caller.inventoryManager.Remove(this);
    }
    public override MiscClass GetMisc() { return this;}
}
