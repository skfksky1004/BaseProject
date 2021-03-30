using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ButtonEx : MonoBehaviour
{
    private Button _button;
    public Button Button
    {
        get
        {
            if (_button == null)
                _button = GetComponent<Button>();

            if (_button == null)
                _button = gameObject.AddComponent<Button>();

            return _button;
        }
    }

    private Image _buttonBg;
    public Image ButtonBG
    {
        get
        {
            //  사용중인 타겟 이미지 확인
            if (_buttonBg == null)
                _buttonBg = Button.targetGraphic.GetComponent<Image>();

            return _buttonBg;
        }
    }

    private TextMeshProUGUI _buttonText;
    public TextMeshProUGUI ButtonText
    {
        get
        {
            //  하위 텍스트 확인
            if (_buttonText == null)
                _buttonText = GetComponentInChildren<TextMeshProUGUI>();

            return _buttonText;
        }
    }

    private Image _buttonIcon;
    public Image ButtonIcon
    {
        get
        {
            //  하위 이미지 확인 (있으면 가지고 옴)
            if (_buttonIcon == null)
            {
                var imgs = gameObject.GetComponentsInChildren<Image>();
                if (imgs.Length > 1)
                {
                    foreach (var i in imgs)
                    {
                        if (i == ButtonBG)
                            continue;

                        _buttonIcon = i;
                    }
                }
            }

            //  하위 없으면 새로 생성
            if (_buttonIcon == null)
            {
                var emptyGo = new GameObject("Icon");
                var img = Instantiate(emptyGo, transform).GetComponent<Image>();
                if (img is Image image)
                {
                    _buttonIcon = image;
                }
            }

            return _buttonIcon;
        }
    }
    
    public bool Interactable => Button.interactable;

    #region TEXT
    
    /// <summary>
    /// 내용 셋팅
    /// </summary>
    /// <param name="str"></param>
    public void SetText(string str)
    {
        if (_buttonText != null)
        {
            _buttonText.SetText(str);
        }
    }
    
    /// <summary>
    ///   내용의 컬러 셋팅(헥사코드)
    /// </summary>
    /// <param name="hexColor"></param>
    public void SetTextColor(string hexColor)
    {
        if (_buttonText != null)
        {
            if(ColorUtility.TryParseHtmlString(hexColor, out var color) == false)
                return;

            _buttonText.color = color;
        }
    }
    
    /// <summary>
    ///   내용의 컬러 셋팅(컬러값)
    /// </summary>
    /// <param name="color"></param>
    public void SetTextColor(Color color)
    {
        if (_buttonText != null)
        {
            _buttonText.color = color;
        }
    }

    #endregion

    #region BG

    /// <summary>
    /// 버튼 자체 이미지 셋팅
    /// </summary>
    /// <param name="spr"></param>
    public void SetButtonBG(Sprite spr)
    {
        if (_buttonBg != null)
        {
            _buttonBg.sprite = spr;
        }
    }

    /// <summary>
    /// 버튼 자체 이미지의 컬러값 셋팅(컬러값)
    /// </summary>
    /// <param name="color"></param>
    private void SetButtonBGColor(Color color)
    {
        if (_buttonBg != null)
        {
            _buttonBg.color = color;
        }
    }

    /// <summary>
    /// 버튼의 자체 이미지 컬러값 셋팅(헥사코드)
    /// </summary>
    /// <param name="hexColor"></param>
    private void SetButtonBGColor(string hexColor)
    {
        if (_buttonBg != null)
        {
            if(ColorUtility.TryParseHtmlString(hexColor, out var color) == false)
                return;
            
            _buttonBg.color = color;
        }
    }

    #endregion
}