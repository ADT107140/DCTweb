using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equiplogic : MonoBehaviour
{
    public static equiplogic sharedInstance = null;

    public GameObject hand = null;
    public GameObject weapon = null;
    public weaponitem current_weapon = null;
    

    [System.Serializable]
    public class weaponitem
    {
        public GameObject prefab = null;
    }
    public void creatweapon(weaponitem item)
    {
       /* if(weapon != null)
        {
            Destroy(weapon);
            current_weapon = null;
        }*/
        GameObject new_weapon = GameObject.Instantiate(item.prefab);
        new_weapon.transform.position = hand.transform.position;
        new_weapon.transform.parent = hand.transform;
        weapon = new_weapon;
        current_weapon = item;
    }
    public weaponitem[] weaponlist = null;

    //public GameObject grid = null;

    private void Awake()
    {
        sharedInstance = this;
    }
    private void Start()
    {
        //grid.transform.GetChild(0).GetComponent<itemOnDrag>().item = weaponlist[0];
    }
   
}
