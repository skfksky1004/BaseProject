using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public override bool Initialize()
    {
        return true;
    }

    protected override void Destroy()
    {
        
    }
}
