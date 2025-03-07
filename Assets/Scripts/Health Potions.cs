using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Health.Heal(1);
        gameObject.SetActive(false);
    }

}
