using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemClass : ScriptableObject // ROOT CLASS
{
    [Header("Item Profile")] // Data shared across every item
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;


    public virtual ItemClass GetItem() { return this; }
    public virtual ToolClass GetTool() { return null; }
    public virtual MiscClass GetMisc() { return null; }

    public virtual void Use(PlayerController caller)
    {
        Debug.Log("Use Item");
    }




}
