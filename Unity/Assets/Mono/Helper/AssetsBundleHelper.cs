using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    public static class AssetsBundleHelper
    {
        public static ValueTuple<UnityEngine.AssetBundle, Dictionary<string, UnityEngine.Object>> LoadBundle(string assetBundleName)
        {
            assetBundleName = assetBundleName.ToLower();
            UnityEngine.AssetBundle assetBundle = null;
            Debug.Log("开始加载dll:" + assetBundleName + " Define.IsAsync = " + Define.IsAsync);
            Dictionary<string, UnityEngine.Object> objects = new Dictionary<string, UnityEngine.Object>();
            
            if (!Define.IsAsync)
            {
                if (Define.IsEditor)
                {
                    string[] realPath = null;
                    realPath = Define.GetAssetPathsFromAssetBundle(assetBundleName);
                    foreach (string s in realPath)
                    {
                        //string assetName = Path.GetFileNameWithoutExtension(s);
                        UnityEngine.Object resource = Define.LoadAssetAtPath(s);
                        objects.Add(resource.name, resource);
                    }
                }
                return (null, objects);
            }
            
            string p = Path.Combine(PathHelper.AppHotfixResPath, assetBundleName);
            //string p = Path.Combine("E:/testdll/", assetBundleName);
            Debug.Log("PathHelper.AppHotfixResPath:" + PathHelper.AppHotfixResPath + " assetBundleName:" + assetBundleName + " p:" + p);
            if (File.Exists(p))
            {
                assetBundle = UnityEngine.AssetBundle.LoadFromFile(p);
            }
            else
            {
                //如果对应目录找不到资源会默认到StreamingAssets目录去找对应的文件
                p = Path.Combine(PathHelper.AppResPath, assetBundleName);
                assetBundle = UnityEngine.AssetBundle.LoadFromFile(p);
            }

            if (assetBundle == null)
            {
                // 获取资源的时候会抛异常，这个地方不直接抛异常，因为有些地方需要Load之后判断是否Load成功
                UnityEngine.Debug.LogWarning($"assets bundle not found: {assetBundleName}");
                return (null, objects);
            }

            UnityEngine.Object[] assets = assetBundle.LoadAllAssets();
            foreach (UnityEngine.Object asset in assets)
            {
                Debug.Log(" asset.name:" + asset.name);
                objects.Add(asset.name, asset);
            }
            return (assetBundle, objects);
        }


        //加载XAsset
        public static ValueTuple<UnityEngine.AssetBundle, Dictionary<string, UnityEngine.Object>> LoadBundleXAsset(string assetBundleName)
        {
            assetBundleName = assetBundleName.ToLower();
            UnityEngine.AssetBundle assetBundle = null;
            Debug.Log("开始加载dll:" + assetBundleName + " Define.IsAsync = " + Define.IsAsync);
            Dictionary<string, UnityEngine.Object> objects = new Dictionary<string, UnityEngine.Object>();

            if (!Define.IsAsync)
            {
                if (Define.IsEditor)
                {
                    string[] realPath = null;
                    realPath = Define.GetAssetPathsFromAssetBundle(assetBundleName);
                    foreach (string s in realPath)
                    {
                        //string assetName = Path.GetFileNameWithoutExtension(s);
                        UnityEngine.Object resource = Define.LoadAssetAtPath(s);
                        objects.Add(resource.name, resource);
                    }
                }
                return (null, objects);
            }

            string p = Path.Combine(PathHelper.AppHotfixResPath, assetBundleName);
            //string p = Path.Combine("E:/testdll/", assetBundleName);
            Debug.Log("PathHelper.AppHotfixResPath:" + PathHelper.AppHotfixResPath + " assetBundleName:" + assetBundleName + " p:" + p);
            if (File.Exists(p))
            {
                assetBundle = UnityEngine.AssetBundle.LoadFromFile(p);
            }
            else
            {
                //如果对应目录找不到资源会默认到StreamingAssets目录去找对应的文件
                p = Path.Combine(PathHelper.AppResPath, assetBundleName);
                assetBundle = UnityEngine.AssetBundle.LoadFromFile(p);
            }

            if (assetBundle == null)
            {
                // 获取资源的时候会抛异常，这个地方不直接抛异常，因为有些地方需要Load之后判断是否Load成功
                UnityEngine.Debug.LogWarning($"assets bundle not found: {assetBundleName}");
                return (null, objects);
            }

            UnityEngine.Object[] assets = assetBundle.LoadAllAssets();
            foreach (UnityEngine.Object asset in assets)
            {
                Debug.Log(" asset.name:" + asset.name);
                objects.Add(asset.name, asset);
            }
            return (assetBundle, objects);
        }
    }
}