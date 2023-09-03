using ET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol : MonoBehaviour
{
    public Vector3 dis;    //存储距离（三维矢量）
    public GameObject ball;    //ball存储要跟随的游戏对象

    void Update()
    {
        if (ball != null)
        {
            transform.position = ball.transform.position - dis;    //自身的坐标等于小球的坐标减去开始的距离
        }
    }

    public void Init() {
        dis = ball.transform.position - transform.position;    //获取自身与小球的距离并赋值给dis
    }
}

