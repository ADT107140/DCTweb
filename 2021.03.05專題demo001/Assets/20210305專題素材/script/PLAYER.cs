using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    Rigidbody person;
    public float speed = 10;
    public GameObject mybag;
    public static bool isopen;
    public GameObject hand;
    public static int status;
    // Start is called before the first frame update
    void Start()
    {
        status = 0;
        person = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float 上下鍵 = Input.GetAxis("Vertical");
        float 左右鍵 = Input.GetAxis("Horizontal");
        剛體移動(上下鍵, 左右鍵);
    }
    void Update()
    {
        openmybag();
        uneqip();
    }
    void 剛體移動(float 垂直, float 水平)
    {
        Vector3 移動量 = new Vector3(水平, 0, 垂直);
        移動量 = 移動量.normalized * -speed * Time.fixedDeltaTime;
        if (移動量 != Vector3.zero)
        {
            person.MovePosition(transform.position + 移動量);
          //  person.MoveRotation(Quaternion.LookRotation(移動量));
        }
        /*else
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);*/
    }
    void openmybag()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isopen = !isopen;
            mybag.SetActive(isopen);
            inventoryManager.RefreshItem();
        }
    }
    void uneqip()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sheep.ball.uncatch();
            
        }
    }
}
