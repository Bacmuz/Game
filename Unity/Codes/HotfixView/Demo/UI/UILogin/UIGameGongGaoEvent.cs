using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIGongGao)]
    public class UIGameGongGaoEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            Debug.Log("开始公告界面..." + UIType.UILogin.StringToAB());
            //await uiComponent.Domain.GetComponent<ResourcesLoaderComponent>().LoadAsync(UIType.UIGongGao.StringToAB());
            //GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIGongGao.StringToAB(), UIType.UIGongGao);
            GameObject bundleGameObject = (GameObject) GameLoadAssetsHelp.LoadUIPrefab(LoadAssets_UIType.UILogin, "UIGameGongGao") ;
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIGongGao, gameObject);
            ui.AddComponent<UIGameGongGaoComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIGongGao.StringToAB());
        }
    }
}