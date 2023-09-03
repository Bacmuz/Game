using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    //public Animator anim;
    private ScrollCircle sc;
    private Vector3 v;
    private Rigidbody rb;
	void Start () {
        sc = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<ScrollCircle>();    //获取ScrollCircle
        rb = this.GetComponent<Rigidbody>();//获取刚体
	}
    void FixedUpdate()
    {
        if (sc.x != 0 || sc.y != 0)    //等于0说明摇杆没有被拉动，所以不要移动和转弯
        {
            v = new Vector3(sc.x, this.transform.position.y, sc.y);    //因为世界坐标中，物体前方的是Z轴，所以我们把ScrollCircle脚本里面的 y 作为角色的Z坐标。
            rb.velocity =1f * transform.forward;    //让角色往前移动
            this.transform.LookAt(v);    //利用LookAt函数让角色转弯
            transform.gameObject.GetComponent<Animator>().Play("run");
        }
        else
        {
            transform.gameObject.GetComponent<Animator>().Play("idle");
        }
    }
}