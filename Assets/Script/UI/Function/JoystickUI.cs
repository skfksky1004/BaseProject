using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickUI : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _imgPad;
    [SerializeField] private Image _imgStick;

    private float _radius = 0;

    private void Awake()
    {
        _radius = _imgPad.rectTransform.sizeDelta.x * 0.5f;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("진입");
        
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("탈출");
    }
}
