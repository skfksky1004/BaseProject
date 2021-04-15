using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://www.youtube.com/watch?v=Biuna856zUA
/// 영상자료
/// </summary>

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite[] sprites;
    public float[,] Grid;

    private int _vertical, _horizontal, _columns, _rows;
    private void Awake()
    {
        _vertical = (int) Camera.main.orthographicSize;
        _horizontal = _vertical * (Screen.width / Screen.height);
        _columns = _horizontal * 2;
        _rows = _vertical * 2;
        
        Grid = new float[_columns,_rows];
    }

    private Vector2 GridToWorldPosition(int x, int y)
    {
        return  new Vector2(x - (_horizontal - 0.5f),y - (_vertical - 0.5f));
    }
    
    private void SpawnTile(int x, int y, float value)
    {
        SpriteRenderer sprRender = Instantiate(tilePrefab, GridToWorldPosition(x, y), Quaternion.identity)
            .GetComponent<SpriteRenderer>();

        sprRender.name = $"x: {x} / y: {y}";
        sprRender.sprite = sprites[IsEdge(x, y)];
    }

    private int IsEdge(int x, int y)
    {
        if (y == _rows - 1)
        {
            if (x == 0)
                return 0;
            if (x != _columns - 1)
                return 1;
            if (x == _columns - 1)
                return 2;
        }
        else if(y != _rows - 1)
        {
            if (x == 0 && y != 0)
                return 3;
            if (x == _columns - 1 && y != 0)
                return 5;
        }
        else if (y == 0)
        {
            if (x == 0)
                return 6;
            if (x != _columns - 1)
                return 7;
            if (x == _columns - 1)
                return 8;
        }
        
        return 4;
    }
}
