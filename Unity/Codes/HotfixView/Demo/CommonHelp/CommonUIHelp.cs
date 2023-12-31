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

            //移除关卡场景,此代码需要在最后,如果在前面移除后面将不会执行
            UIHelper.Remove(scene, UIType.UIGuanQia).Coroutine();

            //添加unit控件
            scene.AddComponent<UnitComponent>();
            //创建进入关卡的数据
            CommonUIHelp.EnterGuanQiaCreate(scene);
        }


        //进入关卡创建对应的角色和UI
        public static async void EnterGuanQiaCreate(Scene scene) {

            await TimerComponent.Instance.WaitAsync(1000);


            //创建主角
            UnitInfo info = new UnitInfo();

            //临时数据,设置主角初始位置
            info.X = -3f;
            info.Y = 1f;
            info.Z = 2f;

            info.UnitId = 1;
            info.ConfigId = 1001;
            Unit unit = UnitFactoryDanJi.CreatePlay(scene, info);



            //GameObject playerObj = MonoBehaviour.Instantiate(GameLoadAssetsHelp.LoadUnit(LoadAssets_UnitType.Player, "Player"));
            //playerObj.transform.position = new Vector3(-1f,- 0.6f, -4.8f);
            //playerObj.AddComponent<Playercontrol>();
            //playerObj.AddComponent<CameraMainTargetComponent>();

            //Unit aa = new Unit();
            //aa.AddComponent<CameraMainTargetComponent>();


            //修改当前主摄像机跟随目标
            Camera.main.transform.GetComponent<Cameracontrol>().ball = unit.GetComponent<GameObjectComponent>().GameObject;
            Camera.main.transform.GetComponent<Cameracontrol>().Init();

            //创建摇杆
            /*
            GameObject YaoGanFather = GameObject.Find("UI");    //获取摇杆的父节点
            GameObject Move = YaoGanFather.transform.Find("UIMove").gameObject;    //通过父节点找到禁用的摇杆UI
            GameObject GongJi = YaoGanFather.transform.Find("UIGongJi").gameObject;
            Move.SetActive(true);    //启用
            GongJi.SetActive(true);
            */

            //创建敌人
            GameObject prefab = GameLoadAssetsHelp.LoadUnit(LoadAssets_UnitType.Monster, "FaShiBear");
            GameObject enemy = UnityEngine.Object.Instantiate(prefab, new Vector3(0.35f, 1f, 24f), Quaternion.identity);
            
            GameObject GongJiprefab = GameLoadAssetsHelp.LoadUIPrefab(LoadAssets_UIType.UIYaoGan, "UIGongJi");
            GameObject GongJigol = UnityEngine.Object.Instantiate(GongJiprefab);
            GameObject Moveprefab = GameLoadAssetsHelp.LoadUIPrefab(LoadAssets_UIType.UIYaoGan, "UIMove");
            GameObject Movegol = UnityEngine.Object.Instantiate(Moveprefab);
            

        }

    }
}


