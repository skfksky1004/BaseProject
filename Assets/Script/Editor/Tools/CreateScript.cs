using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public static class CreateScript
{
    [MenuItem("GameObject/ScriptUI/Create_Script", false, 2)]
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
        sb.AppendLine("using TMPro;");
        sb.AppendLine(string.Empty);

        sb.AppendLine($"public class {scriptName} : MonoBehaviour");
        sb.AppendLine("{");

        // TODO : 자식들이 안찾아진다.
        var selectGo = Selection.activeGameObject;
        var listChilds = selectGo.GetComponentsInChildren<ScriptComponent>();
        foreach (var obj in listChilds)
        {
            if (CheckComponent<TextMeshProUGUI>(obj.gameObject) is TextMeshProUGUI txt)
            {
                var name = txt.name.Replace("(TMP)",string.Empty);
                sb.AppendLine($"\tprivate {nameof(TextMeshProUGUI)} _{name};");
            }

            if (CheckComponent<Image>(obj.gameObject) is Image img)
            {
                var name = img.name;
                sb.AppendLine($"\tprivate {nameof(Image)} _{name};");
            }
        }
        
        sb.AppendLine("}");

        return sb.ToString();
    }

    private static object CheckComponent<T>(GameObject go)
    {
        if (go.GetComponent<T>() is T component)
            return component;

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