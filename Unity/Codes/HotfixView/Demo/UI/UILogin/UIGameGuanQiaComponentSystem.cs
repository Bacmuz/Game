using System;
using System.Net;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ET
{

    [ObjectSystem]
    public class UIGameGuanQiaComponentAwakeSystem : AwakeSystem<UIGameGuanQiaComponent>
    {
        public override void Awake(UIGameGuanQiaComponent self)
        {

            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            self.CloseBtn = rc.Get<GameObject>("CloseBtn");
            self.CloseBtn.GetComponent<Button>().onClick.AddListener(() => { self.OnBtnClose(); });

            self.BtnLevel01 = rc.Get<GameObject>("BtnLevel01");
            self.BtnLevel01.GetComponent<Button>().onClick.AddListener(() => { self.OnBtnLevel01(); });
        }
    }

    [FriendClass(typeof(UIGameGuanQiaComponent))]
    public static class UIGameGuanQiaComponentSystem
    {

        public static async void OnBtnClose(this UIGameGuanQiaComponent self)
        {
            //关闭面板
            await UIHelper.Remove(self.DomainScene(), UIType.UIGuanQia);
        }

        public static void OnBtnLevel01(this UIGameGuanQiaComponent self)
        {
            Debug.Log("我点击了关卡一");

            //SceneManager.LoadScene("Level 1");

            //此处没实际作用,就是随便找个地方演示一下配置表的使用方法
            UnitConfig unitconfig = UnitConfigCategory.Instance.Get(1001);
            Debug.Log("配置表名称：" + unitconfig.Name);
            
            //加载关卡场景
            GameLoadAssetsHelp.LoadAsyncScene("Level 1");

            //加载场景后摇处理的东西
            CommonUIHelp.EnterGuanQiaClearnUI(self.DomainScene());

            
        }

    }
}
