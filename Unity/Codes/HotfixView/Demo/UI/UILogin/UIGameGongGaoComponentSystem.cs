using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{

    [ObjectSystem]
    public class UIGameGongGaoComponentAwakeSystem : AwakeSystem<UIGameGongGaoComponent>
    {

        public override void Awake(UIGameGongGaoComponent self)
        {

            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.LabGongGao = rc.Get<GameObject>("LabGongGao");
            self.LabGongGao.GetComponent<Text>().text = "这是一个游戏的测试公告";

            self.BtnClose = rc.Get<GameObject>("BtnClose");
            self.BtnClose.GetComponent<Button>().onClick.AddListener(() => { self.OnBtnClose(); });


            self.LabUnderGongGao = rc.Get<GameObject>("LabUnderGongGao");
            self.LabUnderGongGao.GetComponent<Text>().text = "玩家交流群:123456789";

            self.GongGaoSpr = rc.Get<GameObject>("GongGaoSpr");
            self.ShowImg();

        }
    }

    [FriendClass(typeof(UIGameGongGaoComponent))]
    public static class UIGameGongGaoComponentSystem
    {

        public static void OnBtnClose(this UIGameGongGaoComponent self) {

            self.Parent.Dispose();

        }
 
        public static void ShowImg(this UIGameGongGaoComponent self)
        {

            //Sprite spr = GameLoadAssetsHelp.LoadSpritePath(LoadAssets_IconType.ItemIcon, "1003");
            //self.GongGaoSpr.GetComponent<Image>().sprite = spr;
            Sprite spr = Resources.Load("1001", typeof(Sprite)) as Sprite;
            self.GongGaoSpr.GetComponent <Image>().sprite = spr;
        }
    }


}
