using System;
using UnityEngine;

namespace ET
{
    public class UnitCreateDanJiEvent : AEvent<EventType.UnitDanJiCreate>
    {
        protected override void Run(EventType.UnitDanJiCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            //GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = GameLoadAssetsHelp.LoadUnit(LoadAssets_UnitType.Player, "Player");
            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            //GameObject go = MonoBehaviour.Instantiate(GameLoadAssetsHelp.LoadUnit(LoadAssets_UnitType.Player, "Player"));
            go.transform.position = args.Unit.Position;
            args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            args.Unit.AddComponent<AnimatorComponent>();
        }
    }
}
