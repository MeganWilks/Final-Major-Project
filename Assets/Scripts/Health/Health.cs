using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health: MonoBehaviour
{
    [Header("Heart Images")]
    [SerializeField] private Texture fullHeart;
    [SerializeField] private Texture halfHeart;
    [SerializeField] private Texture emptyHeart;

    [Header("Heart List")]
    [SerializeField] private List<RawImage> heartList;
    [SerializeField] private int playerHealth = 10;


    public static Health instance;
  
    private void HeartUIUpdate()
    {
        int fullHearts = Mathf.FloorToInt(playerHealth / 2);
        bool halfHearts = playerHealth % 2 == 1;
        for(int i = 0; i < fullHearts; i++)
        {
            heartList[i].texture = fullHeart;
            

        }
        if(halfHearts)
        {
            heartList[fullHearts].texture = halfHeart;
            fullHearts ++;

        }
        for(int i = fullHearts ; i < heartList.Count; i++)
        {
            heartList[i].texture = emptyHeart;


        }
    }
   

    

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    public static void Damage(int damageAmount)
    {
        
        instance.playerHealth -= damageAmount;
        instance.playerHealth =  Mathf.Clamp(instance.playerHealth, 0, 10);
        Debug.Log("health; " + instance.playerHealth);
        instance.HeartUIUpdate();

    }

    public static void Heal(int healAmount)
    {
        
        instance.playerHealth += healAmount;
        instance.playerHealth = Mathf.Clamp(instance.playerHealth, 0, 10);
        Debug.Log("health " + instance.playerHealth);
        instance.HeartUIUpdate();
    }



  
    }

