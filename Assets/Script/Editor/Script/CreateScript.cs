using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CreateScript
{
    [MenuItem("GameObject/CreateScript", false, 1)]
    private static void Create_Script()
    {
        var go = Selection.activeGameObject;
        if (go == null)
            return;

        var content = WriteContent(go.name, string.Empty);
        SaveDataToFile(go.name, content);
    }


    private static string WriteContent(string scriptName, string scriptContent)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System.Collections;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using UnityEngine.UI;");
        sb.AppendLine(string.Empty);

        sb.AppendLine($"public class {scriptName} : MonoBehaviour");
        sb.AppendLine("{");

        // TODO : 자식들이 안찾아진다.
        var listChilds = Selection.activeGameObject.GetComponentsInChildren<GameObject>();
        foreach (var go in listChilds)
        {
            if (CheckComponent<TextMeshProUGUI>(go) != null)
            {
                sb.AppendLine($"\tprivate {nameof(TextMeshProUGUI)} _{go.name}");
            }

            if (CheckComponent<Image>(go) != null)
            {
                sb.AppendLine($"\tprivate {nameof(Image)} _{go.name}");
            }
        }
        
        sb.AppendLine("}");

        return sb.ToString();
    }

    private static object CheckComponent<T>(GameObject go)
    {
        if (go.GetComponent<T>() is T text)
            return text;

        return null;
    }

    //  Json 문자열을 파일로 저장
    private static void SaveDataToFile(string fileName, string content)
    {
        var rootPath = Path.Combine(Application.dataPath, "Script", "UI");
        Directory.CreateDirectory(rootPath);

        //  json파일 저장
        string filePath = Path.Combine(rootPath, $"{fileName}.cs");
        File.WriteAllText(filePath, content);
    }
}