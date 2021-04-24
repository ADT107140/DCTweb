using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useBTN : MonoBehaviour
{
    public GameObject bag;
    public Text thing;
    public GameObject sheep1;
    public GameObject hand;
    
    SphereCollider sheepbody;
    


    private void Start()
    {
        
    }
    private void Update()
    {
        if (PLAYER.status == 1 && Input.GetKeyDown(KeyCode.F5))
        {
            ins_ball();
        }
        
        
     
    }
    public void ItemOnClicked()
    {
        if(thing.text == "羊")
        {
            ins_ball();
            bag.SetActive(false);
            PLAYER.isopen = !PLAYER.isopen;
        }
        
        
    }
    public void ins_ball()
    {
        
        GameObject new_sheep = Instantiate(sheep1.gameObject);
        new_sheep.transform.parent = hand.transform;
        new_sheep.transform.position = hand.transform.position;
        sheep.ball.oncatch();
        new_sheep.GetComponent<SphereCollider>().enabled = false;
        Destroy(new_sheep.GetComponent<Rigidbody>());
        
    }

}
