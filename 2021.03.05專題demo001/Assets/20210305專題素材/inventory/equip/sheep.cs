using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sheep : MonoBehaviour
{
    public Image aim;
    public static sheep ball  = null;
    public bool equip;
    public Rigidbody sheepball;//球
    public Transform basketL;//頭的東東
    float throwForce;//投球力道
    float dis;//距離
    Transform lookBasket;//面向的籃框

    // Start is called before the first frame update

    private void Awake()
    {
        ball = this;
    }
    void Start()
    {
        aim.enabled = false;
        lookBasket = basketL;
        
    }

    // Update is called once per frame
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
    void Update()
    {
        

        dis = Vector3.Distance(lookBasket.position, transform.position);
        throwForce = dis - 0.5f;//根據人物離籃框距離決定投球力道
        if(throwForce > 17)
        {
            throwForce = 10;
        }
        if(equip)
        {
            aim.enabled = true;
           // Cursor.lockState = CursorLockMode.Locked;//鎖定滑鼠游標
           // Cursor.visible = false;//隱藏滑鼠游標
            if (Input.GetButtonUp("Fire2"))
            {
                
                GameObject Ball = GameObject.Find("sheep(Clone)");
                Ball.AddComponent<Rigidbody>();
                Debug.Log(Ball.name);
                Rigidbody ballbody =Ball.GetComponent<Rigidbody>();
                ballbody.GetComponent<SphereCollider>().enabled = true;
                ballbody.GetComponent<SphereCollider>().isTrigger = false;
                ballbody.velocity = transform.TransformDirection(new Vector3(0, 0, throwForce));
                Physics.IgnoreCollision(ballbody.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
                Physics.IgnoreCollision(ballbody.GetComponent<Collider>(), transform.root.root.GetComponent<Collider>());//忽略自身碰撞
                Destroy(Ball, 5);
                StartCoroutine(unequip00());
                
                
            }
        }
        if (!equip)
        {
            GameObject Ball = GameObject.Find("sheep(Clone)");
            aim.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(Ball);
        }
        
    }
    
    public void oncatch()
    {   
        Debug.Log("裝備模式");   
        equip = true;
        PLAYER.status = 1;
        Debug.Log(PLAYER.status + "這邊是");
    } 
    public void uncatch()
    {
        Debug.Log("取消裝備");
        equip = false;
        PLAYER.status = 0;
    }
    IEnumerator unequip00()
    {
        yield return new WaitForSeconds(5);
        uncatch();
        
    }
}
