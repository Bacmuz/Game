using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace ET
{
    public class GameLoadScene : MonoBehaviour
    {

        public IEnumerator LoadScence(string scenceName)
        {
            ResourcePackage package = YooAssets.GetPackage("DefaultPackage");
            string location = "Assets/Scenes/" + scenceName;   //因为demo开启的寻址模式所以不用这里写全路径,如果未开启就需要写全路径
            var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
            bool suspendLoad = false;
            SceneOperationHandle handle = package.LoadSceneAsync(location, sceneMode, suspendLoad);
            yield return handle;
            Debug.Log($"加载场景 {handle.SceneObject.name}");

        }

    }
}
