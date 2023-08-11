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

        public static void OnBtnClose(this UIGameGuanQiaComponent self)
        {
            //关闭面板
            self.Parent.Dispose();
        }

        public static void OnBtnLevel01(this UIGameGuanQiaComponent self)
        {
            Debug.Log("我点击了关卡一");
            SceneManager.LoadScene("Level 1");

        }

    }
}
