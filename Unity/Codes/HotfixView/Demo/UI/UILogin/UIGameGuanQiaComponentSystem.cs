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

            self.BtnLevel01 = rc.Get<GameObject>("BtnLevel02");
            self.BtnLevel01.GetComponent<Button>().onClick.AddListener(() => { self.OnBtnLevel02(); });
        }
    }

    [FriendClass(typeof(UIGameGuanQiaComponent))]
    public static class UIGameGuanQiaComponentSystem
    {

        public static async void OnBtnClose(this UIGameGuanQiaComponent self)
        {
            //�ر����
            await UIHelper.Remove(self.DomainScene(), UIType.UIGuanQia);
        }

        public static void OnBtnLevel01(this UIGameGuanQiaComponent self)
        {
            //�˴�ûʵ������,��������Ҹ��ط���ʾһ�����ñ��ʹ�÷���
            UnitConfig unitconfig = UnitConfigCategory.Instance.Get(1001);
            Debug.Log("���ñ����ƣ�" + unitconfig.Name);
            
            //���عؿ�����
            GameLoadAssetsHelp.LoadAsyncScene("10001");

            //���س�����ҡ����Ķ���
            CommonUIHelp.EnterGuanQiaClearnUI(self.DomainScene());
        }

        public static void OnBtnLevel02(this UIGameGuanQiaComponent self)
        {
            //�˴�ûʵ������,��������Ҹ��ط���ʾһ�����ñ��ʹ�÷���
            UnitConfig unitconfig = UnitConfigCategory.Instance.Get(1001);
            Debug.Log("���ñ����ƣ�" + unitconfig.Name);

            //���عؿ�����
            GameLoadAssetsHelp.LoadAsyncScene("Level 1");

            //���س�����ҡ����Ķ���
            CommonUIHelp.EnterGuanQiaClearnUI(self.DomainScene());
        }

    }
}
