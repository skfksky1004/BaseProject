using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoSingleton<GameSystem>
{
    
    public override bool Initialize()
    {
        return true;
    }

    protected override void Destroy()
    {
        
    }
    
    private IEnumerator Start()
    {
        var isLoad = false;
        isLoad = Initialize();
        yield return new WaitUntil(()=>isLoad);
        
        isLoad = UIManager.I.Initialize();
        UIManager.I.SetParent(transform);
        yield return new WaitUntil(()=>isLoad);
    }
}
