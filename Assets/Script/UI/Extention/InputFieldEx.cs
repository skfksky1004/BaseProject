using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputFieldEx : MonoBehaviour
{
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
    public void UpdateTextLength(string str)
    {
        if(TextLength == null)
            return;

        TextLength.SetText($"{str} / {_inputField.characterLimit}");
    }

    /// <summary>
    /// 내용 셋팅
    /// </summary>
    /// <param name="str"></param>
    public void SetText(string str)
    {
        InputField.text = str;
        
        OnUpdateContent(str);
    }
    
    /// <summary>
    /// 이벤트 셋팅
    /// </summary>
    /// <param name="onEvent"></param>
    public void SetCallBack(UnityAction<string> onEvent)
    {
        InputField.onValueChanged.AddListener(onEvent);
        InputField.onValueChanged.AddListener(OnUpdateContent);
    }
    
    /// <summary>
    /// 내용없을시 나오는 텍스트 문구 셋팅
    /// </summary>
    /// <param name="str"></param>
    public void SetPlaceHolder(string str)
    {
        Placehorder.SetText(str);
    }
    
    /// <summary>
    /// 내용 변경에 따라 스크롤바 이벤트 변경
    /// </summary>
    /// <param name="str"></param>
    private void UpdateScrollBar(string str)
    {
        if(Scrollbar == null)
            return;

        var sizeView = ((RectTransform) transform).rect.size;
        var sizeText = _inputField.textComponent.GetPreferredValues();
        Scrollbar.gameObject.SetActive(sizeView.y < sizeText.y && !string.IsNullOrEmpty(str));
    }

    private void OnUpdateContent(string str)
    {
        UpdateScrollBar(str);
        
        UpdateTextLength(str);
    }
}
