
using UnityEngine;

namespace ET
{
	public class AppStartInitFinish_CreateLoginUI: AEvent<EventType.AppStartInitFinish>
	{
		protected override void Run(EventType.AppStartInitFinish args)
		{
			Debug.Log("创建登录界面...");
			UIHelper.Create(args.ZoneScene, UIType.UILogin, UILayer.Mid).Coroutine();
		}
	}
}
