﻿using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class CommonUIHelp
    {

        //进入关卡时清理界面
        public static void EnterGuanQiaClearnUI(Scene scene)
        {
            //进入了关卡界面需要把登录相关的ui进行移除
            //移除登录界面
            UIHelper.Remove(scene, UIType.UILogin).Coroutine();

            //移除关卡场景,此代码需要再最后,如果在前面移除后面将不会执行
            UIHelper.Remove(scene, UIType.UIGuanQia).Coroutine();

            //添加unit控件
            scene.AddComponent<UnitComponent>();
            //创建进入关卡的数据
            CommonUIHelp.EnterGuanQiaCreate(scene);
        }


        //进入关卡创建对应的角色和UI
        public static async void EnterGuanQiaCreate(Scene scene) {

            await TimerComponent.Instance.WaitAsync(5000);

            //创建主角
            UnitInfo info = new UnitInfo();
            //临时数据,设置主角初始位置
            info.X = -1f;
            info.Y = -0.6f;
            info.Z = -4.8f;
            info.UnitId = 1;
            info.ConfigId = 1001;
            Unit unit = UnitFactoryDanJi.CreatePlay(scene, info);

            //修改当前主摄像机跟随目标
            Camera.main.transform.GetComponent<Cameracontrol>().ball = unit.GetComponent<GameObjectComponent>().GameObject;
            Camera.main.transform.GetComponent<Cameracontrol>().Init();
        }

    }
}
