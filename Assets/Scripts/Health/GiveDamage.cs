using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{


    [SerializeField] public LayerMask Walls;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Health.Damage(1);
            Destroy(gameObject);
            
        }
        
      // if(other.gameObject.layer == Walls)
       // {
       //     Destroy(gameObject);
       // }
            

        
       

      
        
            
        
            

        
                

       
        
    }



}
