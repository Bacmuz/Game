using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;    //添加对状态机的引用
    private Parameter parameter;    //添加对属性的引用
    private float timer;    //计时器

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Idle");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;    //计时器
        if(timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);    //如果达到设定的时间则切换巡逻状态
        }

    }

    public void OnExit()
    {
        timer = 0;    //退出时计时器清零
    }
}

public class PatrolState : IState
{
    private FSM manager;    //添加对状态机的引用
    private Parameter parameter;    //添加对属性的引用
    private int patrolPosition;    //巡逻点

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("run");    //播放运动动画
    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);
        
        manager.transform.position = Vector3.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

        
    }

    public void OnExit()
    {
        
    }
}

public class ChaseState : IState
{
    private FSM manager;    //添加对状态机的引用
    private Parameter parameter;    //添加对属性的引用

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}

public class ReactState : IState
{
    private FSM manager;    //添加对状态机的引用
    private Parameter parameter;    //添加对属性的引用

    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}

public class AttackState : IState
{
    private FSM manager;    //添加对状态机的引用
    private Parameter parameter;    //添加对属性的引用

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}