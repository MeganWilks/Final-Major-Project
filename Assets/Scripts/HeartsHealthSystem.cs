using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsHealthSystem
{

    private List<Heart> heartList;

    public HeartsHealthSystem(int heartsAmount)
    {
        heartList = new List<Heart>();
        for (int i = 0; i < heartsAmount; i++)
        {
           Heart heart = new Heart(2);
           heartList.Add(heart);
        }

        heartList[heartList.Count - 1].SetFragments(3);


    }

    public List<Heart> GetHeartList()
    {
        return heartList;
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


    }
}
