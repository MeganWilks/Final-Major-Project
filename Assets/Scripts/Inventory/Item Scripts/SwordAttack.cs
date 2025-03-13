using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool/Sword")]
public class SwordAttack : ToolClass
{
    public override void Use(PlayerController caller)
    {
        //base.Use(caller);
        Debug.Log("Use Sword");
    }


}
