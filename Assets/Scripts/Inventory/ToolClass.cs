using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool")]
public class ToolClass : ItemClass
{
    [Header("Tools")] // Data Directed towards tools

    public ToolType toolType;
    public enum ToolType

    {
        sword,
        bow
    }

    public override void Use(PlayerController caller)
    {
        base.Use(caller);
        
        if(toolType == ToolType.bow)
        {
            Debug.Log("Arrow Used");
            caller.inventoryManager.Remove(this);
        }
        else if(toolType == ToolType.sword)
        {
            Debug.Log("Sword Used");
        }
        else
        {
            Debug.Log("Use Other");
        }
    }

    public override ToolClass GetTool() { return this; }
    
}
