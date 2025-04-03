using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{

    [SerializeField] bool HasDamaged = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !HasDamaged)
        {
            Debug.Log("HasDamaged");
            Health.Damage(1);
            HasDamaged = true;

        }
        Debug.Log("HasTrigiered");

    }




}
