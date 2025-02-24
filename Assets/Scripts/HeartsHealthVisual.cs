using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;
using CodeMonkey.Utils;

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

    [Header("Conditions")]
    [SerializeField] private bool isHealing;
    [SerializeField] private bool fullyHealed;


    [Header("Cusomise Heart Layout")]
    [SerializeField] private int row = 0;
    [SerializeField] private int column = 0;
    [SerializeField] private int columnMax = 5;
    [SerializeField] private float rowColumnSize = 30f;

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
        FunctionPeriodic.Create(HealingAnimatedPeriodic, 0.5f);

        // CAN CHANGE NUMBER OF HEARTS HERE
        heartsHealthSystem = new HeartsHealthSystem(10); 

        SetHeartsHealthSystem(heartsHealthSystem);
        #endregion

    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        #region SetHeartsHealthSystem Function
        this.heartsHealthSystem = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        //Vector2 heartAnchoredPosition = new Vector2(0, 0);

        for (int i = 0; i < heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            Vector2 heartAnchoredPosition = new Vector2(column * rowColumnSize, row * rowColumnSize);

            CreateHeartImage(heartAnchoredPosition).SetHeartFraments(heart.GetFragmentsAmount());
            //heartAnchoredPosition += new Vector2(10, 0);
            column++;
            if(column > columnMax)
            {
                row++;
                column = 0;
            }
        }
        #endregion

        
        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;


    }
    #region Events
    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        // Hearts health system is dead
        CMDebug.TextPopupMouse("Dead");
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        //Hearts health system was damaged
        //RefreshAllHearts();
        isHealing = true;


    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        //Hearts health system was healed
        RefreshAllHearts();
    }
    #endregion

    private void RefreshAllHearts()
    {
        #region RefreshAllHearts Function
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();

        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartsHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFraments(heart.GetFragmentsAmount());

        }
        #endregion
    }

    private void HealingAnimatedPeriodic()
    {
        #region HealingAnimatedPeriodic Funtion
        if (isHealing) // Making the Loop more efficient by adding a condition 
        {
            bool fullyHealed = true;
            List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();

            for (int i = 0; i < heartImageList.Count; i++)
            {
                HeartImage heartImage = heartImageList[i];
                HeartsHealthSystem.Heart heart = heartList[i];
                if (heartImage.GetFragmentAmount() != heart.GetFragmentsAmount())
                {
                    //Visual Doesnt agree with logic
                    heartImage.AddHeartVisualFragment();
                    fullyHealed = false;
                    break;
                }

            }

            if(fullyHealed)
            {
                isHealing = false;
            }
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
        private int fragments;

        public HeartImage(Image heartImageUI)
        {
            this.heartImageUI = heartImageUI;
        }

        public HeartImage(HeartsHealthVisual heartsHealthVisual,Image heartImage)
        {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
            

        }

        public void  SetHeartFraments(int fragments)
        {
            this.fragments = fragments;
            
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


        public int GetFragmentAmount()
        {
            return fragments;
        }

        public void AddHeartVisualFragment()
        {
            SetHeartFraments(fragments +1);
        }
        #endregion

    }



}
