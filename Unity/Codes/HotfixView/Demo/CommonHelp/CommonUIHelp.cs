using UnityEngine;
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

            //移除关卡场景,此代码需要再最后,如果在前面移除后面将不会执行
            UIHelper.Remove(scene, UIType.UIGuanQia).Coroutine();
        }


        //进入关卡创建对应的角色和UI
        public static void EnterGuanQiaCreate(Scene scene) { 
        
            //修改当前主摄像机跟随目标
            //Camera.main.transform.GetComponent<>

        }

    }
}
