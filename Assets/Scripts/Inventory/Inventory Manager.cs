using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;


public class InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject itemSlots;

    [Header("Inventory Add and Remove")]
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    [SerializeField] public List<SlotClass> items = new List<SlotClass>();
     private GameObject[] slots;


    public void Start()
    {
        slots = new GameObject[itemSlots.transform.childCount];
        // Set all slots
        for(int i = 0; i < itemSlots.transform.childCount; i++)
        {
            slots[i] = itemSlots.transform.GetChild(i).gameObject;
        }

        RefreshUI();

        Add(itemToAdd);
        Remove(itemToRemove);
    }


    public void RefreshUI()
    {
        for(int i = 0; i <slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;

                if (items[i].GetItem().isStackable)
                {
                    slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = items[i].GetQuantity() + "";
                }
                else
                {
                    slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }

            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
            }



        }
        
    }
    public bool Add(ItemClass item)
    {
        //Check Inventory to see if it contains item


        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(1);
        }
        else
        {
            Debug.Log("num items: " +items.Count);
            Debug.Log("Inv size" + slots.Length);
            if(items.Count < slots.Length)
            {
                items.Add(new SlotClass(item, 1));
            }
            else
            {
                return false;

            }
            
           
        }

        RefreshUI();
        return true;
    }

    public bool Remove(ItemClass item)
    {
        #region Remove Item
        SlotClass temp = Contains(item);

        if (temp != null)
        {
            if(temp.GetQuantity() > 1)
            {
                temp.SubQuantity(1);
            }
            else
            {
                SlotClass slotToRemove = new SlotClass();


                foreach (SlotClass slot in items)
                {
                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot;
                        break;
                    }

                }

                items.Remove(slotToRemove);

            }

        }
        else
        {
            return false;
        }
 
        RefreshUI();
        return true;
        #endregion

    }

    public SlotClass Contains(ItemClass item)
    {


        foreach(SlotClass slot in items)
        {
            if(slot.GetItem() == item)
            {
                return slot;
            }
           
        }

        return null;
    }
}
