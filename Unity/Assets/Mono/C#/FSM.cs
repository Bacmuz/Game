using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType    //枚举状态
{
    Idle, Patrol, Chase, React, Attack
}

[Serializable]
public class Parameter    //敌人参数
{
    public int health;    //生命值
    public float moveSpeed;    //移动速度
    public float chaseSpeed;    //追击速度
    public float idleTime;    //停止时间
    public Transform[] patrolPoints;    //巡逻范围
    public Transform[] chasePoints;    //追击范围
    public Animator animator;    //管理动画
}

public class FSM : MonoBehaviour
{
    public Parameter parameter;
    private IState currentState;    ///初始状态
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));

        TransitionState(StateType.Idle);    //设置初始状态为idle

        parameter.animator = GetComponent<Animator>();    //获取动画器

    }
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        if( currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)    //朝向玩家
    {
        if(target != null)
        {
            transform.LookAt(target.transform);
        }
    }
}