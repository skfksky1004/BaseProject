﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 에디터 툴관련 이걸보고 진행
/// https://blog.naver.com/hammerimpact/220772494827
/// </summary>
public class UICreater : ScriptableWizard
{
    private enum eSaveType
    {
        Prefabs,
        Scripts,
    }

    private static UICreater _window;
    private static string aaa;

    private static string _savePath_PrefabKey = "SaveKeyPrefabs";
    private static string _savePath_Prefabs;
    private static string _savePath_ScriptKey = "SaveKeyScript";
    private static string _savePath_Scripts;

    private static eSaveType _saveType = eSaveType.Prefabs;
    private static float _widthMin = 800f;
    private static float _heightMin = 400f;

    private void OnDestroy()
    {
        //  프리팹 위치 저장
        PlayerPrefs.SetString(_savePath_PrefabKey, _savePath_Prefabs);

        //  스크립트 위치 저장
        PlayerPrefs.SetString(_savePath_ScriptKey, _savePath_Scripts);
    }

    [MenuItem("Util/UI/UICreateWindow")]
    private static void OpenWindow()
    {
        //  프리팹 저장 위치
        if (PlayerPrefs.HasKey(_savePath_PrefabKey))
        {
            _savePath_Prefabs = PlayerPrefs.GetString(_savePath_PrefabKey);
            if (string.IsNullOrEmpty(_savePath_Prefabs))
            {
                _savePath_Prefabs = Path.Combine(Application.dataPath, "Resources", "Prefabs");
            }
        }

        //  스크립트 저장 위치
        if (PlayerPrefs.HasKey(_savePath_ScriptKey))
        {
            _savePath_Scripts = PlayerPrefs.GetString(_savePath_ScriptKey);
            if (string.IsNullOrEmpty(_savePath_Scripts))
            {
                _savePath_Scripts = Path.Combine(Application.dataPath, "Script", "UI");
            }
        }

        //  윈도우
        // var window = GetWindow<UICreater>();
        // window.minSize = new Vector2(_widthMin,_heightMin);
        // window.title = "UICreater";
        DisplayWizard<UICreater>("UICreater");
    }

    private void OnGUI()
    {
        View_SavePrefabs();

        View_SaveScript();

        HorizontalView_1();

        VerticalView_4();

        VerticalView_5();
    }

    /// <summary>
    /// 세로 첫번째 - (프리팹 저장 위치)
    /// </summary>
    private static void View_SavePrefabs()
    {
        var xFix = 30;
        var titleSize = new[] {GUILayout.Width(80), GUILayout.Height(xFix)};
        var contentSize = new[] {GUILayout.Height(xFix)};
        var buttonSize = new[] {GUILayout.Width(80), GUILayout.Height(xFix)};
        var style = new GUIStyle
        {
            alignment = TextAnchor.MiddleLeft, 
            border = new RectOffset(2, 2, 2, 2)
        };

        GUILayout.BeginHorizontal();
        {
            //  항목 이름
            GUILayout.Label("프리팹 저장", style, titleSize);

            //  설정된 주소
            GUILayout.Label(_savePath_Prefabs, style, contentSize);

            //  버튼    
            var isClick = GUILayout.Button("변경", buttonSize);
            if (isClick)
            {
                OpenExplorer(eSaveType.Prefabs);
            }
        }
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// 세로 두번째 - (스크립트 저장 위치)
    /// </summary>
    private static void View_SaveScript()
    {
        var xFix = 30;
        var titleSize = new[] {GUILayout.Width(80), GUILayout.Height(xFix)};
        var contentSize = new[] {GUILayout.Height(xFix)};
        var buttonSize = new[] {GUILayout.Width(80), GUILayout.Height(xFix)};
        var style = new GUIStyle();
        style.active.textColor = Color.white;
        style.alignment = TextAnchor.MiddleLeft;
        

        GUILayout.BeginHorizontal();
        {
            //  항목 이름
            GUILayout.Label("스크립트 저장", style, titleSize);

            //  설정된 주소
            GUILayout.Label(_savePath_Scripts, style, contentSize);

            //  버튼    
            var isClick = GUILayout.Button("변경", buttonSize);
            if (isClick)
            {
                OpenExplorer(eSaveType.Scripts);
            }
        }
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// 세로 세번째의 가로 첫번째 - 프리팹 생성기 목록
    /// 세로 세번째의 가로 두번쨰 - 스크립트 변수들 목록
    /// </summary>
    private static void HorizontalView_1()
    {
        //  가로
        GUILayout.BeginHorizontal();
        {
            //  세로_1
            GUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("가로 1");
            }
            GUILayout.EndVertical();

            //  세로_2
            GUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("가로 2");
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// 세로 네번째 - 저장 (작업한 프리팹만 저장)
    /// </summary>
    private static void VerticalView_4()
    {
        GUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("세로 4");
        }
        GUILayout.EndVertical();
    }

    /// <summary>
    /// 세로 다섯번째 - 저장 (작업한 프리팹,스크립트 모두 저장)
    /// </summary>
    private static void VerticalView_5()
    {
        GUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("세로 5");
        }
        GUILayout.EndVertical();
    }

    private static void OpenExplorer(eSaveType saveType)
    {
        if (saveType == eSaveType.Prefabs)
        {
            _savePath_Prefabs =
                EditorUtility.SaveFolderPanel("프리팹 저장 위치", _savePath_Prefabs, "Prefabs");
        }
        else
        {
            _savePath_Scripts =
                EditorUtility.SaveFolderPanel("스크립트 저장 위치", _savePath_Scripts, "Scripts");
        }
    }
}