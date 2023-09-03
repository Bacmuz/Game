using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

namespace ET
{
    public static class LoadAssets_IconType
    {
        public const string ItemIcon = "ItemIcon";          //道具图标
        public const string SkillIcon = "SkillIcon";        //道具图标
    }

    public static class LoadAssets_UIType
    {
        public const string UICommon = "UICommon";      //通用控件
        public const string UILogin = "UILogin";      //通用控件
        public const string UIYaoGan = "UIYaoGan";
    }

    public static class LoadAssets_UnitType
    {
        public const string Monster = "Monster";        //怪物模型
        public const string Player = "Player";          //玩家模型
    }

    public static class LoadAssets_EffectType
    {
        public const string Skill = "Skill";        //技能特效
        public const string Scene = "Scene";        //场景特效
    }

    public static class LoadAssets_VideoType
    {
        public const string Skill = "Skill";        //技能音乐
        public const string Scene = "Scene";        //场景相关音乐
        public const string UI = "UI";              //UI音乐
    }

    public static class GameLoadAssetsHelp
    {
        public static ResourcePackage package;

        //初始化
        public static void InitPackage() {
            if (package == null) {
                package = YooAssets.GetPackage("DefaultPackage");
            }
        }

        //--------------加载图片-------------
        //举例路径:Assets/Bundles/Icon/1001.png
        public static Sprite LoadSpritePath(string typePath,string fileName)
        {
            //初始化值
            InitPackage();
            AssetOperationHandle asshandle = package.LoadAssetSync<Sprite>("Assets/Bundles/Icon/"+ typePath+"/"+ fileName+".png");
            return asshandle.AssetObject as Sprite;
        }

        //--------------加载UI控件--------------
        public static GameObject LoadUIPrefab(string typePath, string fileName)
        {
            //初始化值
            InitPackage();
            AssetOperationHandle codeDllObj = package.LoadAssetSync<GameObject>("Assets/Bundles/UI/" + typePath + "/"+ fileName);
            GameObject dllObj = (GameObject)codeDllObj.AssetObject;
            return dllObj;
        }

        //--------------加载预制件Prefab--------------
        public static GameObject LoadUnit(string typePath, string fileName)
        {
            //初始化值
            InitPackage();
            AssetOperationHandle codeDllObj = package.LoadAssetSync<GameObject>("Assets/Bundles/Unit/" + typePath + "/" + fileName);
            GameObject dllObj = (GameObject)codeDllObj.AssetObject;
            return dllObj;
        }

        //--------------加载预制件Unit--------------
        public static GameObject LoadPrefab(string fileName)
        {
            //初始化值
            InitPackage();
            AssetOperationHandle codeDllObj = package.LoadAssetSync<GameObject>("Assets/Bundles/Prefab/"+ fileName);
            GameObject dllObj = (GameObject)codeDllObj.AssetObject;
            return dllObj;
        }

        //--------------加载预制件Effect--------------
        public static GameObject LoadEffect(string typePath, string fileName)
        {
            //初始化值
            InitPackage();
            AssetOperationHandle codeDllObj = package.LoadAssetSync<GameObject>("Assets/Bundles/Effect/" + typePath + "/" + fileName);
            GameObject dllObj = (GameObject)codeDllObj.AssetObject;
            return dllObj;
        }




        //--------------加载预制件Video--------------
        public static GameObject LoadVideo(string fileName)
        {
            //初始化值
            InitPackage();
            AssetOperationHandle codeDllObj = package.LoadAssetSync<GameObject>("Assets/Bundles/Video/" + fileName);
            GameObject dllObj = (GameObject)codeDllObj.AssetObject;
            return dllObj;
        }


        //--------------加载场景--------------
        public static void LoadAsyncScene(string fileName)
        {
            //初始化值
            InitPackage();
            var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
            bool suspendLoad = false;
            SceneOperationHandle sceneObj = package.LoadSceneAsync("Assets/Scenes/" + fileName, sceneMode, suspendLoad);
            
        }

        //--------------加载配置文件-------------
        public static byte[] LoadConfig(string fileName)
        {
            //初始化值
            InitPackage();

            RawFileOperationHandle handle4 = package.LoadRawFileSync("Assets/Bundles/Config/" + fileName);
            return handle4.GetRawFileData();
            /*
            byte[] fileData = handle4.GetRawFileData();
            string fileText = handle4.GetRawFileText();
            string filePath = handle4.GetRawFilePath();
            */
        }

        //--------------加载配置文件-------------
        public static byte[] LoadCode(string fileName)
        {
            //初始化值
            InitPackage();
            string path = "Assets/Bundles/Code/" + fileName + ".bytes";
            //Debug.Log("path111:" + path);
            RawFileOperationHandle handle4 = package.LoadRawFileSync("Assets/Bundles/Code/" + fileName+ ".bytes");
            //Debug.Log("path222:" + path);
            return handle4.GetRawFileData();
            /*
            byte[] fileData = handle4.GetRawFileData();
            string fileText = handle4.GetRawFileText();
            string filePath = handle4.GetRawFilePath();
            */
        }

    }
}
