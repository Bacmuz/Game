using UnityEngine;
using YooAsset;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.U2D;
using ET;

public class GameHotUpdateMain : MonoBehaviour
{
    /// <summary>
    /// 资源系统运行模式
    /// </summary>
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;


    //控件
    public GameObject ObjUpdateProHint;
    public GameObject ObjUpdateHint;
    public GameObject ObjGameProScr;
    public GameObject ObjVersion;
    public GameObject ObjInit;
    public GameObject ObjUIHotMessageBox;

    public bool UpdateCompleted;


    void Awake()
    {
        Debug.Log($"资源系统运行模式：{PlayMode}");
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        //切换场景时保留此物体
        //DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        /*
        // 官方更新流程
		// 初始化事件系统
		UniEvent.Initalize();

		// 初始化单例系统
		UniSingleton.Initialize();

		// 初始化资源系统
		YooAssets.Initialize();
		YooAssets.SetOperationSystemMaxTimeSlice(30);

		// 创建补丁管理器
		UniSingleton.CreateSingleton<PatchManager>();

		// 开始补丁更新流程
		PatchManager.Instance.Run(PlayMode);
        */

        //自己重写的更新流程
        StartCoroutine(TestLoad());
    }


    //测试加载
    IEnumerator TestLoad()
    {

        //yield return null;
        Debug.Log("开始加载YooAssets...");
        //1. 初始化资源
        YooAssets.Initialize();

        //创建默认的资源包
        var package = YooAssets.CreatePackage("DefaultPackage");

        //设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
        YooAssets.SetDefaultPackage(package);

        if (PlayMode == EPlayMode.EditorSimulateMode)
        {
            //编辑器模式
            var initParameters = new EditorSimulateModeParameters();
            initParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild("DefaultPackage");
            yield return package.InitializeAsync(initParameters);


            //进入ET
            EnterET();
        }
        else if (PlayMode == EPlayMode.HostPlayMode)
        {
            //热更新联机模式
            string defaultHostServer = "http://127.0.0.1:8080/CDN/PC/v2.0.0";
            string fallbackHostServer = "http://127.0.0.1:8080/CDN/PC/v2.0.0";          //备用地址,第一个不好用可以用第二个

            var initParameters = new HostPlayModeParameters();
            initParameters.QueryServices = new GameQueryServices(); //太空战机DEMO的脚本类，详细见StreamingAssetsHelper
            initParameters.DecryptionServices = new GameDecryptionServices();
            initParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
            var initOperation = package.InitializeAsync(initParameters);
            yield return initOperation;

            if (initOperation.Status == EOperationStatus.Succeed)
            {
                Debug.Log("资源包初始化成功！");
            }
            else
            {
                Debug.LogError($"资源包初始化失败：{initOperation.Error}");
            }


            //2. 获取资源版本
            package = YooAssets.GetPackage("DefaultPackage");
            var operation = package.UpdatePackageVersionAsync();
            yield return operation;

            //更新成功
            string packageVersion = operation.PackageVersion;

            if (operation.Status == EOperationStatus.Succeed)
            {
                Debug.Log($"Updated package Version : {packageVersion}");
            }
            else
            {
                //更新失败
                Debug.LogError(operation.Error);
                //直接跳出
                yield break;
            }


            //3. 更新资源清单
            // 更新成功后自动保存版本号，作为下次初始化的版本。
            // 也可以通过operation.SavePackageVersion()方法保存。
            bool savePackageVersion = true;
            var package2 = YooAssets.GetPackage("DefaultPackage");
            var operation2 = package.UpdatePackageManifestAsync(packageVersion, savePackageVersion);
            yield return operation2;

            if (operation2.Status == EOperationStatus.Succeed)
            {
                //4. 开始下载游戏更新资源
                yield return Download();
                //更新成功
                //Debug.Log("游戏热更新已全部完成!!!");

            }
            else
            {
                //更新失败
                Debug.LogError(operation.Error);
                //直接跳出
                yield break;
            }




        }
        else if (PlayMode == EPlayMode.OfflinePlayMode)
        {
            //单机模式
            var initParameters = new OfflinePlayModeParameters();
            yield return package.InitializeAsync(initParameters);
        }
        else if (PlayMode == EPlayMode.WebPlayMode)
        {

            //web模式 暂不添加
        }



    }


    //下载资源
    public IEnumerator DownLoadYooAssetRes(ResourceDownloaderOperation downloader) {

        Debug.Log("开始准备执行热更新...");

        //注册回调方法
        downloader.OnDownloadErrorCallback = OnDownloadErrorFunction;
        downloader.OnDownloadProgressCallback = OnDownloadProgressUpdateFunction;
        downloader.OnDownloadOverCallback = OnDownloadOverFunction;
        downloader.OnStartDownloadFileCallback = OnStartDownloadFileFunction;

        //开启下载
        downloader.BeginDownload();
        yield return downloader;

        //检测下载结果
        if (downloader.Status == EOperationStatus.Succeed)
        {
            //下载成功
            Debug.Log("游戏热更新完成!");
            //UpdateCompleted = true;
            EnterET();
        }
        else
        {
            //下载失败
            Debug.Log("游戏热更新失败..");
        }

    }

    //开始下载资源
    public void StartDownYooAssetRes(ResourceDownloaderOperation downloader)
    {

        Debug.Log("开始更新资源");
        StartCoroutine(DownLoadYooAssetRes(downloader));
    }

    //退出下载资源
    public void ExitDownYooAssetRes() {

        Debug.Log("主动退出游戏下载资源,自动退出游戏!");
        //退出下载数据，退出app
        Application.Quit();
    }


    //进入ET,会在里面热更新代码之类的操作
    public void EnterET() {
        //隐藏热更界面
        this.gameObject.SetActive(false);
        //5. 加载资源
        //更新完成,加载登录场景
        ObjInit.GetComponent<Init>().enabled = true;
    }


    /// <summary>
    /// 远端资源地址查询服务类
    /// </summary>
    private class RemoteServices : IRemoteServices
    {
        private readonly string _defaultHostServer;
        private readonly string _fallbackHostServer;

        public RemoteServices(string defaultHostServer, string fallbackHostServer)
        {
            _defaultHostServer = defaultHostServer;
            _fallbackHostServer = fallbackHostServer;
        }
        string IRemoteServices.GetRemoteFallbackURL(string fileName)
        {
            return $"{_defaultHostServer}/{fileName}";
        }
        string IRemoteServices.GetRemoteMainURL(string fileName)
        {
            return $"{_fallbackHostServer}/{fileName}";
        }
    }

    /// <summary>
    /// 资源文件解密服务类
    /// </summary>
    /// 
    
    private class GameDecryptionServices : IDecryptionServices
    {
        public ulong LoadFromFileOffset(DecryptFileInfo fileInfo)
        {
            return 32;
        }

        public byte[] LoadFromMemory(DecryptFileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        
        public Stream LoadFromStream(DecryptFileInfo fileInfo)
        {
            BundleStream bundleStream = new BundleStream(fileInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return bundleStream;

        }
        

        public uint GetManagedReadBufferSize()
        {
            return 1024;
        }
    }
    

    IEnumerator Download()
    {
        int downloadingMaxNum = 10;
        int failedTryAgain = 3;
        var package = YooAssets.GetPackage("DefaultPackage");
        var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);

        //没有需要下载的资源
        if (downloader.TotalDownloadCount == 0)
        {
            UpdateCompleted = true;
            EnterET();
            yield break;
        }

        //需要下载的文件总数和总大小
        int totalDownloadCount = downloader.TotalDownloadCount;
        long totalDownloadBytes = downloader.TotalDownloadBytes;

        //实例化一个弹框,让玩家确认是否下载热更数据
        GameObject messageObj = MonoBehaviour.Instantiate(ObjUIHotMessageBox);
        messageObj.transform.SetParent(ObjUpdateHint.transform.parent.transform);
        messageObj.transform.localPosition = new Vector3(0, 0, 0);
        messageObj.transform.localScale = new Vector3(1, 1, 1);
        messageObj.GetComponent<UIHotMessageBox>().LabMessage.GetComponent<Text>().text = "本次更新大小:" + ShowUpdate(totalDownloadBytes);
        messageObj.GetComponent<UIHotMessageBox>().onEvOk.AddListener(() => { StartDownYooAssetRes(downloader); });
        messageObj.GetComponent<UIHotMessageBox>().onEvCancel.AddListener(() => { ExitDownYooAssetRes(); });
  
    }

    /// <summary>
    /// 开始下载文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="sizeBytes"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnStartDownloadFileFunction(string fileName, long sizeBytes)
    {
        //throw new NotImplementedException();
        Debug.Log("开始下载:" + fileName + " 大小:" + sizeBytes);
        ObjUpdateHint.GetComponent<Text>().text = "开始下载:" + fileName + " 大小:" + sizeBytes;
    }

    /// <summary>
    /// 下载完成
    /// </summary>
    /// <param name="isSucceed"></param>
    private void OnDownloadOverFunction(bool isSucceed)
    {
        //throw new NotImplementedException();
        Debug.Log("下载完成:" + isSucceed);
    }

    /// <summary>
    /// 更新中
    /// </summary>
    /// <param name="totalDownloadCount"></param>
    /// <param name="currentDownloadCount"></param>
    /// <param name="totalDownloadBytes"></param>
    /// <param name="currentDownloadBytes"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnDownloadProgressUpdateFunction(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
    {
        //throw new NotImplementedException();
        Debug.Log("更新中,文件总数:" + totalDownloadCount + "已下载数量:" + currentDownloadCount + "下载总大小:" + totalDownloadBytes + "已下载大小:" + currentDownloadBytes);
        ObjUpdateProHint.GetComponent<Text>().text = "更新中,文件总数:" + totalDownloadCount + "已下载数量:" + currentDownloadCount + "下载总大小:" + totalDownloadBytes + "已下载大小:" + currentDownloadBytes;
    }


    /// <summary>
    /// 下载报错
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="error"></param>
    /// <exception cref="NotImplementedException"></exception>

    private void OnDownloadErrorFunction(string fileName, string error)
    {
        Debug.Log("下载失败:" + fileName + " error:" + error);
        //throw new NotImplementedException();
    }

    //展示更新
    private string ShowUpdate(long zijie) {

        long kbs = (long)(zijie / 1024);
        float mbs = (float)kbs / 1024f;

        if (mbs < 1)
        {
            return kbs + "KB";
        }
        else {
            return mbs.ToString("F2") + "MB";
        }
    }
}