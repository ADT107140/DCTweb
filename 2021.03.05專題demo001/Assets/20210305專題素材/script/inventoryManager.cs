using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryManager : MonoBehaviour
{
    static inventoryManager instance;

    public inventory mybag;
    public GameObject slotgrid;
    //public Slot slotprefab;
    public GameObject emptyslot;
    public Text iteminformation;
    public static int index;

    public List<GameObject> slots = new List<GameObject>();

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void OnEnable()
    {
        RefreshItem();
        instance.iteminformation.text = "";
    }

    public static void UpdateitemInfo(string itemdescription)
    {
        instance.iteminformation.text = itemdescription;
    }

    /*public static void CreateNewItem(item item)
    {
        Slot newItem = Instantiate(instance.slotprefab, instance.slotgrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotgrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }*/
    public static void RefreshItem()//先刪掉後創回來
    {
        for(int i = 0;i < instance.slotgrid.transform.childCount; i++) //先執行
        {
            if (instance.slotgrid.transform.childCount == 0)
                break;
           // Debug.Log(instance.slotgrid.transform.GetChild(i).gameObject.name);
            Destroy(instance.slotgrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();//使slot不會生成在外面
        }
        for(int i =0; i<instance.mybag.itemlist.Count; i++) //算是以ITEMLIST有的東西來做製作 後執行
        {
            instance.slots.Add(Instantiate(instance.emptyslot));
            instance.slots[i].transform.SetParent(instance.slotgrid.transform);
            instance.slots[i].GetComponent<Slot>().slotId = i;
            index = i;
            instance.slots[i].GetComponent<Slot>().setupSlot(instance.mybag.itemlist[i]);
           // CreateNewItem(instance.mybag.itemlist[i]);
           // Debug.Log(instance.mybag.itemlist[i].name);
        }
    }
    
}
