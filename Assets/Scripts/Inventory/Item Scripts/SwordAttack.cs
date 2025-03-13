using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool/Sword")]
public class SwordAttack : ToolClass
{

    [Header("Sword Object")]

    [SerializeField] public GameObject sword;
    public override void Use(PlayerController caller)
    {
        //base.Use(caller);
        Debug.Log("Use Sword");
        Instantiate(sword, caller.transform.position, Quaternion.identity);
        
    }


}
