using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputFieldEx : MonoBehaviour
{
    private CanvasScaler _canvasScaler = null;
    private RectTransform _canvasRect = null;
    
    private TMP_InputField _inputField;
    public TMP_InputField InputField
    {
        get
        {
            if (_inputField == null)
                _inputField = GetComponent<TMP_InputField>();

            return _inputField;
        }
    }

    private TextMeshProUGUI _text;
    public TextMeshProUGUI Text
    {
        get
        {
            if (_text == null)
                _text = InputField.textComponent.GetComponent<TextMeshProUGUI>();

            return _text;
        }
    }

    private TextMeshProUGUI _placehorder;
    public TextMeshProUGUI Placehorder
    {
        get
        {
            if (_placehorder == null)
                _placehorder = InputField.placeholder.GetComponent<TextMeshProUGUI>();

            return _placehorder;
        }
    }

    private Scrollbar _scrollbar;
    private Scrollbar Scrollbar
    {
        get
        {
            if (_scrollbar == null)
                _scrollbar = InputField.verticalScrollbar;

            return _scrollbar;
        }
    }

    public bool Interactable => InputField.interactable;
    
    
    public TextMeshProUGUI TextLength;
    
    /// <summary>
    /// 내용 셋팅
    /// </summary>
    /// <param name="str">내용</param>
    public void SetText(string str)
    {
        InputField.text = str;
        
        OnUpdateContent(str);
    }
    
    /// <summary>
    /// 이벤트 셋팅
    /// </summary>
    /// <param name="onEvent">내용이 변경될때마다 실행될 이벤트</param>
    public void SetCallBack(UnityAction<string> onEvent)
    {
        InputField.onValueChanged.AddListener(onEvent);
        InputField.onValueChanged.AddListener(OnUpdateContent);
    }
    
    /// <summary>
    /// 내용없을시 나오는 텍스트 문구 셋팅
    /// </summary>
    /// <param name="str">내용없을때 나오는 글</param>
    public void SetPlaceHolder(string str)
    {
        Placehorder.SetText(str);
    }
    
    /// <summary>
    /// 내용 변경에 따라 스크롤바 이벤트 변경
    /// </summary>
    /// <param name="str">내용</param>
    private void UpdateScrollBar(string str)
    {
        if(Scrollbar == null)
            return;

        var sizeView = ((RectTransform) transform).rect.size;
        var sizeText = _inputField.textComponent.GetPreferredValues();
        Scrollbar.gameObject.SetActive(sizeView.y < sizeText.y && !string.IsNullOrEmpty(str));
    }
    
    /// <summary>
    /// 내용 단어 갯수 표시
    /// </summary>
    /// <param name="str">내용</param>
    public void UpdateTextLength(string str)
    {
        if(TextLength == null)
            return;

        TextLength.SetText($"{str} / {_inputField.characterLimit}");
    }

    /// <summary>
    /// 스크롤바 및 글자 갯수 갱신
    /// </summary>
    /// <param name="str">내용</param>
    private void OnUpdateContent(string str)
    {
        UpdateScrollBar(str);
        
        UpdateTextLength(str);
    }

    /// <summary>
    /// 키보드 사이즈 높이
    /// 사용주의 : 코루틴으로 0.3초뒤 확인필요
    /// </summary>
    /// <returns></returns>
    private float GetNativeKeyboardHeight()
    {
        //  실 기기에 적용된 UI해상도 알아 내기
        if (_canvasScaler == null)
            _canvasScaler = GetComponentInParent<CanvasScaler>();
        
        if (_canvasRect == null)
            _canvasRect = _canvasScaler.GetComponent<RectTransform>();
        
        float height = 0;

#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var unityPlayer = unityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
            var view = unityPlayer.Call<AndroidJavaObject>("getView");
            var dialog = unityPlayer.Get<AndroidJavaObject>("mSoftInputDialog");
        
            if (view == null || dialog == null)
                return 0;
        
            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                view.Call("getWindowVisibleDisplayFrame", Rct);
        
                var keyboradNativeSize = Mathf.Round(Screen.height - Rct.Call<int>("height"));
                
                var per = keyboradNativeSize / Screen.height;
                height = _canvasRect.sizeDelta.y * per;
                // Debug.Log($"키보드 높이 : {height}");
                return height;
            }
        }
#else
        height = Mathf.Round(TouchScreenKeyboard.area.height);
        // height = Screen.height * 0.5f - (height >= Display.main.systemHeight ? 0f : height);

        // Debug.Log($"키보드 높이 : {height}");
        return height;
#endif
    }
}
