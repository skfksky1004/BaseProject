using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class Editor_Control_GameSpeed : MonoBehaviour
{
    public enum SpeedType
    {
        None,
        VerySlow,
        Slow,
        Normal,
        Fast,
        VeryFast,
    }
    
    private Vector2 _initPos;
    private Vector2 _buttonSize = new Vector2(40, 20);

#if UNITY_EDITOR
    private GUIStyle _inActiveStyle =  new GUIStyle();
    private GUIStyle _ActiveStyle = new GUIStyle();
#endif

    private SpeedType _selectSpeedType = SpeedType.Normal;

    private void Awake()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            var compList = this.GetComponents<MonoBehaviour>();
            foreach (var c in compList)
            {
                if (c is Editor_Control_GameSpeed editor)
                {
                    DestroyImmediate(editor);
                }
            }
        }
    }

    [Conditional("UNITY_EDITOR")]
    private void OnEnable()
    {
        _initPos = new Vector2(EditorWindow.focusedWindow.position.width, EditorWindow.focusedWindow.position.height);
    
        _inActiveStyle.normal.textColor = Color.gray;
        _ActiveStyle.normal.textColor = Color.green;
        
        DontDestroyOnLoad(this);
    }
    
    [Conditional("UNITY_EDITOR")]
    private void OnGUI()
    {
        if (EditorWindow.focusedWindow != null)
        {
            _initPos = new Vector2(Screen.width, Screen.height);
        }

        var posY = _initPos.y;
        var isVerySlowSpeed = GUI.Button(new Rect(10, posY - 50, _buttonSize.x, _buttonSize.y), 
            $"x0.2",
            _selectSpeedType == SpeedType.VerySlow ? _ActiveStyle : _inActiveStyle);
        
        if (isVerySlowSpeed)
        {
            OnGameSpeed_VerySlow();
        }
        
        var isSlowSpeed = GUI.Button(new Rect(10, posY - 80, _buttonSize.x, _buttonSize.y), 
            $"x0.5",
            _selectSpeedType == SpeedType.Slow ? _ActiveStyle : _inActiveStyle);
        
        if (isSlowSpeed)
        {
            OnGameSpeed_Slow();
        }

        var isNormalSpeed = GUI.Button(new Rect(10, posY - 110, _buttonSize.x, _buttonSize.y), 
            $"x1.0",
            _selectSpeedType == SpeedType.Normal ? _ActiveStyle : _inActiveStyle);
        
        if (isNormalSpeed)
        {
            OnGameSpeed_Normal();
        }

        var isFastSpeed = GUI.Button(new Rect(10, posY - 140, _buttonSize.x, _buttonSize.y), 
            $"x1.5",
            _selectSpeedType == SpeedType.Fast ? _ActiveStyle : _inActiveStyle);
        
        if (isFastSpeed)
        {
            OnGameSpeed_Fast();
        }
        

        var isVeryFastSpeed = GUI.Button(new Rect(10, posY - 170, _buttonSize.x, _buttonSize.y), 
            $"x2.0",
            _selectSpeedType == SpeedType.VeryFast ? _ActiveStyle : _inActiveStyle);
        
        if (isVeryFastSpeed)
        {
            OnGameSpeed_VeryFast();
        }
    }

    [Conditional("UNITY_EDITOR")]
    private void OnGameSpeed_VerySlow()
    {
        Time.timeScale = 0.2f;
        _selectSpeedType = SpeedType.VerySlow;
    }

    [Conditional("UNITY_EDITOR")]
    private void OnGameSpeed_Slow()
    {
        Time.timeScale = 0.5f;
        _selectSpeedType = SpeedType.Slow;
    }
    
    [Conditional("UNITY_EDITOR")]
    private void OnGameSpeed_Normal()
    {
        Time.timeScale = 1f;
        _selectSpeedType = SpeedType.Normal;
    }
    
    [Conditional("UNITY_EDITOR")]
    private void OnGameSpeed_Fast()
    {
        Time.timeScale = 1.5f;
        _selectSpeedType = SpeedType.Fast;
    }

    [Conditional("UNITY_EDITOR")]
    private void OnGameSpeed_VeryFast()
    {
        Time.timeScale = 2.0f;
        _selectSpeedType = SpeedType.VeryFast;
    }
}