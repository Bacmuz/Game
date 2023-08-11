using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UIGuanQia)]
    public class UIGameGuanQiaEvent : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            Debug.Log("开始公告界面..." + UIType.UILogin.StringToAB());
            await uiComponent.Domain.GetComponent<ResourcesLoaderComponent>().LoadAsync(UIType.UIGuanQia.StringToAB());
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UIGuanQia.StringToAB(), UIType.UIGuanQia);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIGuanQia, gameObject);
            ui.AddComponent<UIGameGuanQiaComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            ResourcesComponent.Instance.UnloadBundle(UIType.UIGuanQia.StringToAB());
        }
    }
}