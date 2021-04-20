using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    private enum ProgressType
    {
        NONE,
        DAMAGE,       //  데미지
        DOTDAMAGE,    //  데미지(도트)
        HEALING,      //  회복
        DOTHEALING,   //  회복(도트)
    }
    
    [SerializeField] private Image _imgBackground;
    [SerializeField] private Image _imgShadow;
    [SerializeField] private Image _imgBase;

    private float _baseValue = 1;
    private float _shadowValue = 1;
    private float _tickValue = 0;
    private float _damageValue = 0;

    private float _delayTime = 0.4f;
    private ProgressType _curState = ProgressType.NONE;

    private void Awake()
    {
        InitProgress();
    }

    public void InitProgress(bool isMax = true)
    {
        _baseValue = 1;
        _shadowValue = 1;

        _imgBase.fillAmount = _baseValue;
        _imgShadow.fillAmount = _shadowValue;
    }
    
    /// <summary>
    ///  바 깍기
    /// </summary>
    /// <param name="damageValue"></param>
    public void Damage(float damageValue)
    {
        _curState = ProgressType.DAMAGE;
        _damageValue = damageValue;
        
        ProgressStart();
    }

    /// <summary>
    ///  지속적으로 깍기
    /// </summary>
    /// <param name="tickValue"></param>
    public void DotDamage(float tickValue)
    {
        _curState = ProgressType.DOTDAMAGE;
        _tickValue = tickValue;
        
        ProgressStart();
    }
    
    /// <summary>
    ///  회복하기
    /// </summary>
    /// <param name="damageValue"></param>
    public void Healing(float damageValue)
    {
        _curState = ProgressType.HEALING;
        _damageValue = damageValue;
        
        ProgressStart();
    }

    /// <summary>
    ///  지속적으로 회복하기
    /// </summary>
    /// <param name="tickValue"></param>
    public void DotHealing(float tickValue)
    {
        _curState = ProgressType.DOTHEALING;
        _tickValue = tickValue;

        ProgressStart();
    }

    private void ProgressStart()
    {
        StopAllCoroutines();
        StartCoroutine(Progress());
    }
    
    private IEnumerator Progress()
    {
        bool isContinue = true;
        while (isContinue)
        {
            bool isZero = _baseValue <= 0 && _shadowValue <= 0 && (_curState == ProgressType.DAMAGE || _curState == ProgressType.DOTDAMAGE);
            bool isFull = _baseValue >= 1 && _shadowValue >= 1 && (_curState == ProgressType.HEALING || _curState == ProgressType.DOTHEALING);
            if ( isZero || isFull)
            {
                isContinue = false;
            }
            
            switch (_curState)
            {
                case ProgressType.DAMAGE:
                {
                    if (_damageValue != 0)
                    {
                        _baseValue -= _damageValue;
                        _damageValue = 0;
                    }

                    if (_baseValue < _shadowValue && _baseValue > 0)
                        _shadowValue -= 0.01f;
                
                    yield return new WaitForEndOfFrame();
                    
                    break;
                }
                case ProgressType.DOTDAMAGE:
                {
                    if (_baseValue > 0)
                    {
                        _baseValue -= _tickValue;
                        _shadowValue -= _tickValue;
                        
                        yield return new WaitForEndOfFrame();
                    }
                    
                    break;
                }
                case ProgressType.HEALING:
                {
                    
                    
                    _baseValue += _damageValue;
                    _shadowValue = _baseValue;
                    
                    _damageValue = 0;
                    isContinue = false;
                    
                    break;
                }
                case ProgressType.DOTHEALING:
                {
                    if (_baseValue < 1)
                    {
                        _baseValue += _tickValue;
                        _shadowValue = _baseValue;
                        
                        yield return new WaitForEndOfFrame();
                    }
                
                    break;
                }
                case ProgressType.NONE:
                default:
                {
                    isContinue = false;
                    break;
                }
            }

            UpdateImage();
            
            //  
            if (_shadowValue == 1)
            {
                _curState = ProgressType.NONE;
                isContinue = false;
                
                break;
            }
        }
    }

    private void UpdateImage()
    {
        _imgBase.fillAmount = _baseValue;
        _imgShadow.fillAmount = _shadowValue;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage(0.4f);
        }
        else  if (Input.GetKeyDown(KeyCode.W))
        {
            DotDamage(0.01f);
        }
        else  if (Input.GetKeyDown(KeyCode.E))
        {
            Healing(0.1f);
        }
        else  if (Input.GetKeyDown(KeyCode.R))
        {
            DotHealing(0.01f);
        }
    }
}