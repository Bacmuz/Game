using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class CameraMainTargetComponent: Entity, IAwake,IUpdate, IDestroy
    {
        public Vector3 dis;    //存储距离（三维矢量）
        public GameObject ball;    //ball存储要跟随的游戏对象
    }
}
