using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask Avoid_destroy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Health.Damage(1);
        }

        if(other.gameObject.layer != Avoid_destroy)
        {
            Destroy(gameObject);
        }
    }
}
