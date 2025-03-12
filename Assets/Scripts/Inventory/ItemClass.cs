using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemClass : ScriptableObject // ROOT CLASS
{
    [Header("Item Profile")] // Data shared across every item
    public string itemName;
    public Sprite itemIcon;


    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
    public abstract MiscClass GetMisc();

    

    
}
