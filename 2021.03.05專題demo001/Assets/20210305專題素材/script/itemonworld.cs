using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemonworld : MonoBehaviour
{
    public item thisitem;
    public inventory playerinventory;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             AddNewItem();
             Destroy(gameObject);   
            
        }
    }
    public void AddNewItem()
    {
        if(!playerinventory.itemlist.Contains(thisitem))
        {
            //playerinventory.itemlist.Add(thisitem);
            // inventoryManager.CreateNewItem(thisitem);
            for(int i = 0;i < playerinventory.itemlist.Count;i++)
            {
                if(playerinventory.itemlist[i] == null)
                {
                    playerinventory.itemlist[i] = thisitem;
                    thisitem.itemHeld += 1;
                    break;
                }
            
            }
        }
        else
        {
            thisitem.itemHeld += 1;
        }
        inventoryManager.RefreshItem();
    }
}
