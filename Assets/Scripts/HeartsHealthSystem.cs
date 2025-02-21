using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartsHealthSystem
{

    public event EventHandler OnDamaged;
    private List<Heart> heartList;

    public HeartsHealthSystem(int heartsAmount)
    {
        heartList = new List<Heart>();
        for (int i = 0; i < heartsAmount; i++)
        {
           Heart heart = new Heart(2);
           heartList.Add(heart);
        }

    }

    public List<Heart> GetHeartList()
    {
        return heartList;
    }


    public void Damage(int damageAmount)
    {
        //Goes through all hearts from start to end
        for (int i = heartList.Count - 1; i >= 0; i--)
        { 
            Heart heart = heartList[i];
            // Tests whether the heart can take the damageAmount
            if (damageAmount > heart.GetFragmentsAmount())
            {
                //Heart cannot take the damageAmount, the heart is damadaged and keeps going till the next heart
                damageAmount -= heart.GetFragmentsAmount();
                heart.Damage(heart.GetFragmentsAmount());
            }
            else
            {
                // heart can take the full damage amount, heart takes the damage and stops the loop
                heart.Damage(damageAmount);
                // Stops Loop
                break;
            }
        }

        if  (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }

    //Resembles 1 Heart
    public class Heart
    {
        private int fragments;

        public Heart (int fragments)
        {
            this.fragments = fragments;
        }

        public int GetFragmentsAmount()
        {
            return fragments;
        }

        public void SetFragments (int fragments)
        {
            this.fragments = fragments;
        }

        public void Damage( int  damageAmount)
        {
            if(damageAmount >= fragments)
            {
                fragments = 0;
            }
            else
            {
                fragments -= damageAmount;
            }
        }


    }
}
