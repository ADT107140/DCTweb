using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mission : MonoBehaviour
{

    public static bool missioncomplete;
    public Text schedule;
    public item need;
    int num;
    

    // Start is called before the first frame update
    void Start()
    {
        missioncomplete = false;
        
        schedule.text = num + "/3";
   
    }

    // Update is called once per frame
    void Update()
    {
        num = need.itemHeld;

        Debug.Log(num);
        if (need.itemHeld < 3)
        {
            schedule.text = num + "/3";
        }
        else
        {
            schedule.text = num + "/3(完成)";
            missioncomplete = true;
            
        }
        
    }
    

}
