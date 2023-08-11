using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[ComponentOf(typeof(UI))]
	public class UILoginComponent: Entity, IAwake
	{
		public GameObject account;
		public GameObject password;
		public GameObject loginBtn;

		//游戏公告按钮
		public GameObject BtnGongGao;
		//游戏开始按钮
		public GameObject BtnStart;

    }
}