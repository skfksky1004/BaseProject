using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpUI : MonoBehaviour
{
    [SerializeField] private ProgressUI[] _listBars;
    [SerializeField] private ProgressUI _mainBar;

    private Queue<ProgressUI> _queue = new Queue<ProgressUI>();

    private int _destHp;   //  목표 체력
    [SerializeField] private int _curHp;    //  현재 체력
    [SerializeField] private int _maxHp;    //  맥스 체력
    
    private int _curLineCount;    //  현재 체력의 남은 라인 갯수
    
    private const int MaxOneLine = 1000;

    public void InitHp(int Hp)
    {
        if(_listBars == null || _listBars.Length == 0)
            return;
        
        _curHp = _maxHp = Hp;
        _curLineCount = _curHp / MaxOneLine;

        //  뷰를 큐에 적립;
        foreach (var bar in _listBars)
        {
            bar.gameObject.SetActive(true);
            bar.InitProgress();
            
            _queue.Enqueue(bar);
        }

        SetMainBar();
    }

    public void AddDamage(int damage)
    {
        _destHp = _curHp - damage;
        
        PlayProgress();
    }

    private void PlayProgress()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeHp());
    }

    private IEnumerator ChangeHp()
    {
        var remainPer = ((_curHp - _destHp) / (float)MaxOneLine);    //  0.1f;
        //  목표 체력보다 현재 체력이 더 많은 상황
        while (remainPer > 0)
        {
            //  1. 현재 남아잇는 체력을 깍는다.
            //  2. 남은 값이 한라인의 값보다 많으면 한라인을 통으로 깍는다.
            //  3. 남아있는 값이 한라인의 값보다 작으면 서서히 깍는다.

            var mainValue = _mainBar.NormalValue; //  0.1f;
            if (mainValue < remainPer)
            {
                _mainBar.ChangeBar_Sub(mainValue);
                
                remainPer -= mainValue;
                _curLineCount -= 1;
                
                yield return new WaitUntil(() => _mainBar.IsContinue);
            }
            else if (remainPer > 1)
            {
                _mainBar.ChangeBar_Sub(1);
                
                remainPer -= 1;
                _curLineCount -= 1;
                
                yield return new WaitUntil(() => _mainBar.IsContinue);
            }
            // else if(_curLineCount <= 1)
            // {
            //     _mainBar.ChangeBar_Sub(remainPer);
            //     remainPer -= _mainBar._followValue;
            //     yield return new WaitUntil(() => !_mainBar.IsContinue);
            // }
            else
            {
                _mainBar.ChangeBar_Sub(remainPer);
                remainPer = _mainBar._followValue;
                yield return new WaitUntil(() => _mainBar.IsContinue);
            }

            ChangeBar();
            
        }
        
        UpdateHp();
    }

    private void UpdateHp()
    {
        _curHp = _destHp;
    }
    
    private void ChangeBar()
    {
        if (_mainBar.NormalValue <= 0 && 
            _mainBar.FollowValue <= 0)
        {
            SetRemainBar();

            SetMainBar();
        }
    }

    private void SetMainBar()
    {
        if (_curHp <= 0)
        {
            _mainBar.StopAllCoroutines();
            _mainBar.gameObject.SetActive(false);
            return;
        }
        
        _mainBar = null;
        _mainBar = _queue.Dequeue();
        _mainBar.InitProgress(true, _curLineCount > 1);
        _mainBar.transform.SetAsLastSibling();

        if (_curLineCount < 1)
        {
            foreach (var bar in _listBars)
            {
                if (_mainBar != bar)
                {
                    bar.StopAllCoroutines();
                    bar.gameObject.SetActive(false);
                }
            }
        }
    }

    private void SetRemainBar()
    {
        _mainBar.transform.SetAsFirstSibling();
        _mainBar.InitProgress();
        _queue.Enqueue(_mainBar);
    }

    public int Damage = 2000;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InitHp(10000);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            AddDamage(Damage);
        }
        
    }
}
