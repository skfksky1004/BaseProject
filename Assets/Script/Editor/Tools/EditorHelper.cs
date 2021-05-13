using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorHelper
{
    public static GUIStyle LabelStyle = new GUIStyle(GUI.skin.label)
    {
        alignment = TextAnchor.MiddleLeft
    };

    public static GUIStyle ButtonStyle = new GUIStyle(GUI.skin.button)
    {
        alignment = TextAnchor.MiddleCenter
    };

    public static GUILayoutOption[] GuiOption(float width = 0, float height = 0)
    {
        return new[] {GUILayout.Width(width), GUILayout.Height(height)};
    }
    
    public static GUILayoutOption[] GuiOption_Width(float width = 0)
    {
        return new[] {GUILayout.Width(width), GUILayout.ExpandHeight(true)};
    }
    
    public static GUILayoutOption[] GuiOption_height(float height = 0)
    {
        return new[] {GUILayout.ExpandWidth(true), GUILayout.Height(height)};
    }

    
    public static void GuiLine(Color color, int height = 1, int width = 0)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, height);
        
        // rect.height = i_height;
        if (width > 0)
        {
            rect.width = width;
            rect.x = width;
        }
        
        color.a = 1;  //  알파값 고정
        EditorGUI.DrawRect(rect, color);
    }
}
