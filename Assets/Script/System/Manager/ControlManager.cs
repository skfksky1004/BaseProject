using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ControlEvent
{

}

public class ControlManager : MonoSingleton<ControlManager>
{
    public override bool Initialize()
    {
        return true;
    }

    protected override void Destroy()
    {
        
    }
    
}
