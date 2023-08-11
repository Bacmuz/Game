#if UNITY_EDITOR
using HybridCLR.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;


    public static class CopyAotDll
    {
        [MenuItem("HybridCLR/CopyAotDlls")]
        public static void CopyAotDllMeth()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            string fromDir = Path.Combine(HybridCLRSettings.Instance.strippedAOTDllOutputRootDir, target.ToString());
            string toDir = "Assets/Bundles/AotDlls";
            if (Directory.Exists(toDir))
            {
                Directory.Delete(toDir, true);
            }
            Directory.CreateDirectory(toDir);
            AssetDatabase.Refresh();

            foreach (string aotDll in HybridCLRSettings.Instance.patchAOTAssemblies)
            {
                File.Copy(Path.Combine(fromDir, aotDll), Path.Combine(toDir, $"{aotDll}.bytes"), true);
            }

            // 设置ab包
            AssetImporter assetImporter = AssetImporter.GetAtPath(toDir);
            assetImporter.assetBundleName = "AotDlls.unity3d";
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

# endif
