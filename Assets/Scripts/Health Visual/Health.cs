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
  
    private void HeartUIUpdate(int health)
    {
        int fullHearts = Mathf.FloorToInt(health / 2);
        bool halfHearts = health % 2 == 1;
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
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }
}
