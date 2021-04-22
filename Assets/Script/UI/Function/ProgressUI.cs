using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    private enum ProgressType
    {
        NONE,
        Subtract, //  데미지
        Add, //  회복
    }

    [SerializeField] private Image _imgBackground;
    [SerializeField] private Image _imgFollow;
    [SerializeField] private Image _imgNormal;

    public float _normalValue = 1;
    public float _followValue = 1;
    public float _changeValue = 0;

    private bool _bContunue;
    private float _delayTime = 0.01f;
    private ProgressType _curState = ProgressType.NONE;

    public float NormalValue => _normalValue;
    public float FollowValue => _followValue;

    public bool IsContinue => _bContunue;
    private void Awake()
    {
        InitProgress();
    }

    public void InitProgress(bool isMax = true, bool isFollow = true)
    {
        _imgBackground.gameObject.SetActive( false);
        
        _normalValue = isMax ? 1 : 0;
        _followValue = isMax ? 1 : 0;

        _imgNormal.fillAmount = _normalValue;

        _imgFollow.gameObject.SetActive(isFollow);
        _imgFollow.fillAmount = _followValue;
    }

    /// <summary>
    ///  바 깍기
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBar_Sub(float value)
    {
        _curState = ProgressType.Subtract;
        _changeValue = value;

        //  값이 1이상이면 follow 속도록 증가
        _delayTime = value >= 1f ? 0.1f : 0.02f ;
        
        ProgressStart();
    }

    /// <summary>
    ///  바 채우기
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBar_Add(float value)
    {
        _curState = ProgressType.Add;
        _changeValue = value;

        ProgressStart();
    }

    private void ProgressStart()
    {
        if(isActiveAndEnabled == false)
            return;
        
        StopAllCoroutines();
        StartCoroutine(Progress());
    }

    private IEnumerator Progress()
    {
        _bContunue = true;
        while (_bContunue)
        {
            bool isZero = _normalValue <= 0 && _followValue <= 0 && _curState == ProgressType.Subtract;
            bool isFull = _normalValue >= 1 && _followValue >= 1 && _curState == ProgressType.Add;
            if (isZero || isFull)
            {
                _bContunue = false;
                yield break;
            }

            switch (_curState)
            {
                case ProgressType.Subtract:
                {
                    if (_changeValue != 0)
                    {
                        _normalValue -= _changeValue;
                        _changeValue = 0;
                    }

                    if (_normalValue < _followValue && _normalValue >= 0)
                        _followValue -= (_delayTime);

                    yield return new WaitForSeconds(Time.deltaTime);

                    break;
                }
                case ProgressType.Add:
                {
                    _normalValue += _changeValue;
                    _followValue = _normalValue;

                    _changeValue = 0;
                    _bContunue = false;

                    break;
                }
            }

            UpdateImage();
        }
    }

    private void UpdateImage()
    {
        _imgNormal.fillAmount = _normalValue;
        _imgFollow.fillAmount = _followValue;
    }
}