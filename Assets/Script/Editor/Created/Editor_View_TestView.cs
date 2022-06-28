using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.SceneManagement;

public class Editor_View_TestView : EditorWindow
{
    [MenuItem("Window/UIElements/Editor_View_TestView")]
    public static void ShowExample()
    {
        Editor_View_TestView wnd = GetWindow<Editor_View_TestView>();
        wnd.titleContent = new GUIContent("Editor_View_TestView");

        SceneView secondSceneView = ScriptableObject.CreateInstance<SceneView>();
        secondSceneView.Show();

        //var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
    }

    public void OnSceneGUI()
    {
        Handles.BeginGUI();

        if (GUILayout.Button("Press Me"))
            Debug.Log("Got it to work.");

        Handles.EndGUI();
    }
}