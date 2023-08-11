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
    /// ��Դϵͳ����ģʽ
    /// </summary>
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;


    //�ؼ�
    public GameObject ObjUpdateProHint;
    public GameObject ObjUpdateHint;
    public GameObject ObjGameProScr;
    public GameObject ObjVersion;
    public GameObject ObjInit;
    public GameObject ObjUIHotMessageBox;

    public bool UpdateCompleted;


    void Awake()
    {
        Debug.Log($"��Դϵͳ����ģʽ��{PlayMode}");
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        //�л�����ʱ����������
        //DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        /*
        // �ٷ���������
		// ��ʼ���¼�ϵͳ
		UniEvent.Initalize();

		// ��ʼ������ϵͳ
		UniSingleton.Initialize();

		// ��ʼ����Դϵͳ
		YooAssets.Initialize();
		YooAssets.SetOperationSystemMaxTimeSlice(30);

		// ��������������
		UniSingleton.CreateSingleton<PatchManager>();

		// ��ʼ������������
		PatchManager.Instance.Run(PlayMode);
        */

        //�Լ���д�ĸ�������
        StartCoroutine(TestLoad());
    }


    //���Լ���
    IEnumerator TestLoad()
    {

        //yield return null;
        Debug.Log("��ʼ����YooAssets...");
        //1. ��ʼ����Դ
        YooAssets.Initialize();

        //����Ĭ�ϵ���Դ��
        var package = YooAssets.CreatePackage("DefaultPackage");

        //���ø���Դ��ΪĬ�ϵ���Դ��������ʹ��YooAssets��ؼ��ؽӿڼ��ظ���Դ�����ݡ�
        YooAssets.SetDefaultPackage(package);

        if (PlayMode == EPlayMode.EditorSimulateMode)
        {
            //�༭��ģʽ
            var initParameters = new EditorSimulateModeParameters();
            initParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild("DefaultPackage");
            yield return package.InitializeAsync(initParameters);


            //����ET
            EnterET();
        }
        else if (PlayMode == EPlayMode.HostPlayMode)
        {
            //�ȸ�������ģʽ
            string defaultHostServer = "http://127.0.0.1:8080/CDN/PC/v2.0.0";
            string fallbackHostServer = "http://127.0.0.1:8080/CDN/PC/v2.0.0";          //���õ�ַ,��һ�������ÿ����õڶ���

            var initParameters = new HostPlayModeParameters();
            initParameters.QueryServices = new GameQueryServices(); //̫��ս��DEMO�Ľű��࣬��ϸ��StreamingAssetsHelper
            initParameters.DecryptionServices = new GameDecryptionServices();
            initParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
            var initOperation = package.InitializeAsync(initParameters);
            yield return initOperation;

            if (initOperation.Status == EOperationStatus.Succeed)
            {
                Debug.Log("��Դ����ʼ���ɹ���");
            }
            else
            {
                Debug.LogError($"��Դ����ʼ��ʧ�ܣ�{initOperation.Error}");
            }


            //2. ��ȡ��Դ�汾
            package = YooAssets.GetPackage("DefaultPackage");
            var operation = package.UpdatePackageVersionAsync();
            yield return operation;

            //���³ɹ�
            string packageVersion = operation.PackageVersion;

            if (operation.Status == EOperationStatus.Succeed)
            {
                Debug.Log($"Updated package Version : {packageVersion}");
            }
            else
            {
                //����ʧ��
                Debug.LogError(operation.Error);
                //ֱ������
                yield break;
            }


            //3. ������Դ�嵥
            // ���³ɹ����Զ�����汾�ţ���Ϊ�´γ�ʼ���İ汾��
            // Ҳ����ͨ��operation.SavePackageVersion()�������档
            bool savePackageVersion = true;
            var package2 = YooAssets.GetPackage("DefaultPackage");
            var operation2 = package.UpdatePackageManifestAsync(packageVersion, savePackageVersion);
            yield return operation2;

            if (operation2.Status == EOperationStatus.Succeed)
            {
                //4. ��ʼ������Ϸ������Դ
                yield return Download();
                //���³ɹ�
                //Debug.Log("��Ϸ�ȸ�����ȫ�����!!!");

            }
            else
            {
                //����ʧ��
                Debug.LogError(operation.Error);
                //ֱ������
                yield break;
            }




        }
        else if (PlayMode == EPlayMode.OfflinePlayMode)
        {
            //����ģʽ
            var initParameters = new OfflinePlayModeParameters();
            yield return package.InitializeAsync(initParameters);
        }
        else if (PlayMode == EPlayMode.WebPlayMode)
        {

            //webģʽ �ݲ����
        }



    }


    //������Դ
    public IEnumerator DownLoadYooAssetRes(ResourceDownloaderOperation downloader) {

        Debug.Log("��ʼ׼��ִ���ȸ���...");

        //ע��ص�����
        downloader.OnDownloadErrorCallback = OnDownloadErrorFunction;
        downloader.OnDownloadProgressCallback = OnDownloadProgressUpdateFunction;
        downloader.OnDownloadOverCallback = OnDownloadOverFunction;
        downloader.OnStartDownloadFileCallback = OnStartDownloadFileFunction;

        //��������
        downloader.BeginDownload();
        yield return downloader;

        //������ؽ��
        if (downloader.Status == EOperationStatus.Succeed)
        {
            //���سɹ�
            Debug.Log("��Ϸ�ȸ������!");
            //UpdateCompleted = true;
            EnterET();
        }
        else
        {
            //����ʧ��
            Debug.Log("��Ϸ�ȸ���ʧ��..");
        }

    }

    //��ʼ������Դ
    public void StartDownYooAssetRes(ResourceDownloaderOperation downloader)
    {

        Debug.Log("��ʼ������Դ");
        StartCoroutine(DownLoadYooAssetRes(downloader));
    }

    //�˳�������Դ
    public void ExitDownYooAssetRes() {

        Debug.Log("�����˳���Ϸ������Դ,�Զ��˳���Ϸ!");
        //�˳��������ݣ��˳�app
        Application.Quit();
    }


    //����ET,���������ȸ��´���֮��Ĳ���
    public void EnterET() {
        //�����ȸ�����
        this.gameObject.SetActive(false);
        //5. ������Դ
        //�������,���ص�¼����
        ObjInit.GetComponent<Init>().enabled = true;
    }


    /// <summary>
    /// Զ����Դ��ַ��ѯ������
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
    /// ��Դ�ļ����ܷ�����
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

        //û����Ҫ���ص���Դ
        if (downloader.TotalDownloadCount == 0)
        {
            UpdateCompleted = true;
            EnterET();
            yield break;
        }

        //��Ҫ���ص��ļ��������ܴ�С
        int totalDownloadCount = downloader.TotalDownloadCount;
        long totalDownloadBytes = downloader.TotalDownloadBytes;

        //ʵ����һ������,�����ȷ���Ƿ������ȸ�����
        GameObject messageObj = MonoBehaviour.Instantiate(ObjUIHotMessageBox);
        messageObj.transform.SetParent(ObjUpdateHint.transform.parent.transform);
        messageObj.transform.localPosition = new Vector3(0, 0, 0);
        messageObj.transform.localScale = new Vector3(1, 1, 1);
        messageObj.GetComponent<UIHotMessageBox>().LabMessage.GetComponent<Text>().text = "���θ��´�С:" + ShowUpdate(totalDownloadBytes);
        messageObj.GetComponent<UIHotMessageBox>().onEvOk.AddListener(() => { StartDownYooAssetRes(downloader); });
        messageObj.GetComponent<UIHotMessageBox>().onEvCancel.AddListener(() => { ExitDownYooAssetRes(); });
  
    }

    /// <summary>
    /// ��ʼ�����ļ�
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="sizeBytes"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnStartDownloadFileFunction(string fileName, long sizeBytes)
    {
        //throw new NotImplementedException();
        Debug.Log("��ʼ����:" + fileName + " ��С:" + sizeBytes);
        ObjUpdateHint.GetComponent<Text>().text = "��ʼ����:" + fileName + " ��С:" + sizeBytes;
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="isSucceed"></param>
    private void OnDownloadOverFunction(bool isSucceed)
    {
        //throw new NotImplementedException();
        Debug.Log("�������:" + isSucceed);
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="totalDownloadCount"></param>
    /// <param name="currentDownloadCount"></param>
    /// <param name="totalDownloadBytes"></param>
    /// <param name="currentDownloadBytes"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnDownloadProgressUpdateFunction(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
    {
        //throw new NotImplementedException();
        Debug.Log("������,�ļ�����:" + totalDownloadCount + "����������:" + currentDownloadCount + "�����ܴ�С:" + totalDownloadBytes + "�����ش�С:" + currentDownloadBytes);
        ObjUpdateProHint.GetComponent<Text>().text = "������,�ļ�����:" + totalDownloadCount + "����������:" + currentDownloadCount + "�����ܴ�С:" + totalDownloadBytes + "�����ش�С:" + currentDownloadBytes;
    }


    /// <summary>
    /// ���ر���
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="error"></param>
    /// <exception cref="NotImplementedException"></exception>

    private void OnDownloadErrorFunction(string fileName, string error)
    {
        Debug.Log("����ʧ��:" + fileName + " error:" + error);
        //throw new NotImplementedException();
    }

    //չʾ����
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