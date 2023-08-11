using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[ObjectSystem]
	public class UILoginComponentAwakeSystem : AwakeSystem<UILoginComponent>
	{

		public override void Awake(UILoginComponent self)
		{
			ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

			self.loginBtn = rc.Get<GameObject>("LoginBtn");
			self.loginBtn.GetComponent<Button>().onClick.AddListener(()=> { self.OnLogin(); });

			self.account = rc.Get<GameObject>("Account");
			self.password = rc.Get<GameObject>("Password");
			
			//打开游戏公告
			self.BtnGongGao = rc.Get<GameObject>("BtnGongGao");
            self.BtnGongGao.GetComponent<Button>().onClick.AddListener(() => { self.OnGongGao(); });

			//开始游戏，打开选关界面
            self.BtnStart= rc.Get<GameObject>("BtnStart");
			self.BtnStart.GetComponent<Button>().onClick.AddListener(() => { self.OnStart(); });
        }
	}

	[FriendClass(typeof(UILoginComponent))]
	public static class UILoginComponentSystem
	{
		public static void OnLogin(this UILoginComponent self)
		{
			LoginHelper.Login(
				self.DomainScene(),
				ConstValue.LoginAddress,
				self.account.GetComponent<InputField>().text,
				self.password.GetComponent<InputField>().text).Coroutine();
		}


		public static async void OnGongGao(this UILoginComponent self) {

			UIHelper.Create(self.ZoneScene(), UIType.UIGongGao, UILayer.Mid).Coroutine();

			/*
			GameObject ObjGonggaoUI = MonoBehaviour.Instantiate(GameLoadAssetsHelp.LoadUIPrefab(LoadAssets_UIType.UILogin, "UIGameGongGao"));
			ObjGonggaoUI.transform.SetParent(self.Parent.GetComponent<GameObjectComponent>().GameObject.transform);
			ObjGonggaoUI.transform.localScale = new Vector3(1, 1, 1);

			ObjGonggaoUI.AddComponent<UIGameGongGaoComponent>();
			*/

		}

        public static async void OnStart(this UILoginComponent self)
        {
            UIHelper.Create(self.ZoneScene(), UIType.UIGuanQia, UILayer.Mid).Coroutine();
        }
    }
}
