using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class BuildPlayer : MonoBehaviour
{
    [MenuItem("Build/Build_AOS")]
    public static void Build_AOS()
    {
        var savePath = $"Build/AOS";
        Build(BuildTarget.Android, BuildOptions.Development, savePath);
    }

    [MenuItem("Build/Build_iOS")]
    public static void Build_iOS()
    {
        var savePath = $"Build/IOS";
        Build(BuildTarget.iOS, BuildOptions.Development, savePath);
    }

    private static void Build(BuildTarget buildTarget, BuildOptions buildOptions, string savePath)
    {
        var buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = FindEnabledEditorScenes();
        buildPlayerOptions.locationPathName = savePath;
        buildPlayerOptions.target = buildTarget;
        buildPlayerOptions.options = buildOptions;

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if(summary.result == BuildResult.Succeeded)
        {
            //  Secceed
        }
        else if(summary.result == BuildResult.Failed)
        {
            //  Failed
        }
    }

    private static string[] FindEnabledEditorScenes()
    {
        var editorScenes = new List<string>();
        foreach(var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled = false)
                continue;

            editorScenes.Add(scene.path);
        }

        return editorScenes.ToArray();
    }
}
