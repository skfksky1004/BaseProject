using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// https://wergia.tistory.com/94 : 원하는 번들 만들기
/// https://ssabi.tistory.com/12?category=851607 : 번들 만들기
/// </summary>

public static class CreateAssetBundle
{
    private static string _saveDirectory = Application.dataPath + "AssetBundles";
    
    [MenuItem("Util/Bundle/Create Bundle")]
    private static void Create()
    {
        if (!Directory.Exists(_saveDirectory))
        {
            Directory.CreateDirectory(_saveDirectory);
        }

        var buildTarget = BuildTarget.Android;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            buildTarget = BuildTarget.iOS;
        }

        BuildPipeline.BuildAssetBundles(
            _saveDirectory,
            BuildAssetBundleOptions.StrictMode,
            buildTarget);
    }

    // private static void BuildAssetBundles()
    // {
    //     if (!Directory.Exists(_saveDirectory))
    //     {
    //         Directory.CreateDirectory(_saveDirectory);
    //     }
    //     
    //     var bundleName = Resources.
    // }
}
