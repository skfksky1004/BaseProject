using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public static class AddScriptComponent
{
    [MenuItem("GameObject/ScriptUI/Add_ScriptComponent", false, 2)]
    private static void Add_ScriptComponent()
    {
        var go = Selection.activeGameObject;
        if (go == null)
            return;
        
        go.AddComponent<ScriptComponent>();
    }
}

