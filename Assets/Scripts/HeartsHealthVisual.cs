using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour
{
    #region Variables
    [Header("Heart Sprites")]

    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    [Header("Lists")]

    [SerializeField] private List<HeartImage> heartImageList;

    [Header("Scripts")]

    [SerializeField] private HeartsHealthSystem heartsHealthSystem;
    #endregion

    private void Awake()
    {
        #region Awake
        heartImageList = new List<HeartImage>();
        #endregion
    }



    private void Start()
    {
        #region Start
        heartsHealthSystem = new HeartsHealthSystem(3);
        SetHeartsHealthSystem(heartsHealthSystem);
        #endregion

    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        #region SetHeartsHealthSystem Function
        this.heartsHealthSystem = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        Vector2 heartAnchoredPosition = new Vector2(0, 0);

        for (int i = 0; i < heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFraments(heart.GetFragmentsAmount());
            heartAnchoredPosition += new Vector2(10, 0);
        }
        #endregion

    }


    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        #region Creating Heart Visual
        //Creating the game Object
        GameObject heartGameObject = new GameObject("Heart",typeof(Image));

        // Set as child of the transform
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;

        //Find and Size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30,30);

        //Set heart Sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = emptyHeartSprite;

        HeartImage heartImage = new HeartImage (this, heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
        #endregion

    }


    // Resembles 1 Heart
    public class HeartImage
    {
        #region HeartImageFunction
        [Header("Variables")]

        [SerializeField] Image heartImage;
        [SerializeField] private HeartsHealthVisual heartsHealthVisual;
        private Image heartImageUI;

        public HeartImage(Image heartImageUI)
        {
            this.heartImageUI = heartImageUI;
        }

        public HeartImage(HeartsHealthVisual heartsHealthVisual,Image heartImage)
        {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
            

        }

        public void  SetHeartFraments(int  fragments)
        {
            
            switch (fragments)
            {
                case 0: 
                    heartImage.sprite = heartsHealthVisual.emptyHeartSprite;
                    break;
                case 1: 
                    heartImage.sprite = heartsHealthVisual.halfHeartSprite; 
                    break;
                case 2:
                    heartImage.sprite = heartsHealthVisual.fullHeartSprite;
                    break;
            }
        }
        #endregion

    }



}
