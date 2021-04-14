using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    public override bool Initialize()
    {
        return true;
    }

    protected override void Destroy()
    {
        throw new System.NotImplementedException();
    }
}
