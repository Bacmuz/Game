using UnityEngine;

namespace ET
{
    public static class UnitFactoryDanJi
    {
        public static Unit CreatePlay(Scene currentScene, UnitInfo unitInfo)
        {
            //设置当前Unit里的缓存
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
            unitComponent.Add(unit);

            //设定位置
            unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
            unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);

            //添加数值组件
            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            for (int i = 0; i < unitInfo.Ks.Count; ++i)
            {
                numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
            }

            //抛出事件
            Game.EventSystem.Publish(new EventType.UnitDanJiCreate() { Unit = unit });

            return unit;
        }

    }
}