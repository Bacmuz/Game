using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Firecontrol : MonoBehaviour {
    public float Shootspeed = 1.5f;  //子弹速度
    private Playercontrol player;    //玩家
    public bool isTouch = false;    //判断是否产生碰撞
    public float timer = 0.0f;    //计时器
    public GameObject touch;    //发生碰撞的物体
	
	void Update () {
        timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Shootspeed  * Time.deltaTime);
        if(isTouch == true)
        {
            Destroy(gameObject);    //产生碰撞销毁自身
            if(touch.tag == "Player")
            {
                touch.GetComponent<Playercontrol>().GetHit();    //碰到玩家，玩家掉血
            }
            else if(touch.tag == "Cube")
            {
                Destroy(touch);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        isTouch = true;    //产生碰撞参数变为true
        touch = other.gameObject;    //获取发生碰撞的物体
    }
}