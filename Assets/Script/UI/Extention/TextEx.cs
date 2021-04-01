using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class TextEx : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    public TextMeshProUGUI TextMesh
    {
        get
        {
            if (_textMesh == null)
                _textMesh = GetComponent<TextMeshProUGUI>();

            if (_textMesh == null)
                _textMesh = gameObject.AddComponent<TextMeshProUGUI>();

            return _textMesh;
        }
    }
    
    /// <summary>
    /// 내용 적용 및 내용에 맞게 사이즈 조정
    /// </summary>
    /// <param name="str">내용</param>
    /// <param name="isReSizeX">X 적용 유무</param>
    /// <param name="isResizeY">Y 적용 유무</param>
    public void SetText(string str, bool isReSizeX = false, bool isResizeY = false)
    {
        TextMesh.SetText(str);

        //  조건에 따라 텍스트 내용에 맞게 사이즈 조정
        var size = TextMesh.rectTransform.sizeDelta;
        TextMesh.rectTransform.sizeDelta = new Vector2(
            isReSizeX ? TextMesh.preferredWidth : size.x, 
            isResizeY ? TextMesh.preferredHeight : size.y);
    }

    public void SetMargin(Vector4 margin)
    {
        TextMesh.margin = margin;
    }
}
