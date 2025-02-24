using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health: MonoBehaviour
{

    [SerializeField] Texture fullHeart;
    [SerializeField] Texture halfHeart;
    [SerializeField] Texture emptyHeart;

    [SerializeField] List<RawImage> heartList;
    [SerializeField] int playerHealth = 10;


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
        Debug.Log("health; " + instance.playerHealth);
        instance.HeartUIUpdate();

    }



  
    }

