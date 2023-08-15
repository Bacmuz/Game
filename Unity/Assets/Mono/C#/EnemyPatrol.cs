/*using System.Security.Principal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolSpeed = 0.6f;    //巡逻速度
    private NavMeshAgent agent;    //导航组件
    private float patrolTimer = 0f;   //计时器    
    public GameObject[] points = null;    //巡逻点
    private int num = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Partrolling();
    }

    public void Partrolling()
    {
        agent.isStopped = false;
        agent.speed = patrolSpeed;
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            //此时敌人停止，计时器开始计时
            transform.gameObject.GetComponent<Animator>().Play("idle");
            patrolTimer += Time.deltaTime;
            //等待完毕，敌人可以继续行动，找到下一个目标点，重置计时器
                if(num == 3)
                {
                    num = 0;
                }
                else
                {
                    num++;
                }
                patrolTimer = 0;
        }        
        
        //agent.destination = patrolWalPoints.GetChild(wayPointIndex).position;    //走向巡逻点
        transform.LookAt(points[num].transform.position);
        agent.SetDestination(points[num].transform.position);    //走向巡逻点
        transform.gameObject.GetComponent<Animator>().Play("run");    //播放运动动画
    }
}*/