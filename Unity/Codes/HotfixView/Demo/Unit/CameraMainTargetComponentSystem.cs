using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class CameraMainTargetComponentAwakeSystem : AwakeSystem<CameraMainTargetComponent>
    {
        public override void Awake(CameraMainTargetComponent self)
        {
            self.dis = self.ball.transform.position - self.Parent.GetComponent<GameObjectComponent>().GameObject.transform.position;    //获取自身与小球的距离并赋值给dis
        }

    }

    public class CameraMainTargetComponentUpdateSystem : UpdateSystem<CameraMainTargetComponent>
    {
        public override void Update(CameraMainTargetComponent self)
        {
            self.Update();
        }
    }

    [ObjectSystem]
    public class CameraMainTargetComponentDestroySystem : DestroySystem<CameraMainTargetComponent>
    {
        public override void Destroy(CameraMainTargetComponent self)
        {

        }
    }


    [FriendClass(typeof(CameraMainTargetComponent))]
    public static class CameraMainTargetComponentSystem {

        public static void Update(this CameraMainTargetComponent self) {

            if (self.ball != null)
            {
                self.Parent.GetComponent<GameObjectComponent>().GameObject.transform.position = self.ball.transform.position - self.dis;    //自身的坐标等于小球的坐标减去开始的距离
            }
        }

    }

}
