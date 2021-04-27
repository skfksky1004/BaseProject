using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFindManager : MonoSingleton<PathFindManager>
{
    private Grid _grid;
    private Tilemap _tilemap;
    
    public override bool Initialize()
    {
        if (_grid == null)
        {
            _grid = FindObjectOfType<Grid>();
        }
        
        return true;
    }

    protected override void Destroy()
    {
    }

    private void Awake()
    {
    }
}
