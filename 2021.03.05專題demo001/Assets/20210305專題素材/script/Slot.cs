using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotId;//空格id等於物品id
    public item slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    public GameObject itemInSlot;
    public void ItemOnClicked()
    {
        inventoryManager.UpdateitemInfo(slotInfo);
    }
    public void  setupSlot(item item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
        
    }

}
