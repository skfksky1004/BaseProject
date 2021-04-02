using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GlobalConst;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Transform Tf => this.transform;

    private Rigidbody2D rb2D;

    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _jumpSpeed = 10;

    private int _jumpStep = 0;
    private Vector2 _pos;

    private void Awake()
    {
        if (rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();

        _pos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!IsCheckBlock(Vector2.left))
            {
                _pos.y = Tf.position.y;
                MoveVec(Vector2.left);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!IsCheckBlock(Vector2.right))
            {
                _pos.y = Tf.position.y;
                MoveVec(Vector2.right);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
    }

    private void LateUpdate()
    {
        if (_jumpStep > 0)
        {
            if (IsCheckBlock (Vector2.down)) 
            {
                _jumpStep = 0;
            }
        }
    }

    /// <summary>
    /// 움직임
    /// </summary>
    /// <param name="vec"></param>
    private void MoveVec(Vector2 vec)
    {
        // Tf.Translate(vec * (_moveSpeed * Time.deltaTime));
        _pos += vec * (_moveSpeed * Time.deltaTime);
        Tf.localPosition = _pos;
    }

    /// <summary>
    /// 벽 체크
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private bool IsCheckBlock(Vector2 dir)
    {
        var ridus = Tf.localScale.x * 0.5f;
        var cols = Physics2D.RaycastAll(Tf.position, dir, ridus);

        //  나 자신과 자신이외(벽, 천장, 바닥)
        return cols != null && cols.Length >= 2;
    }

    private void PlayerJump()
    {
        if (_jumpStep < GameConst.JumpLimit)
        {
            _jumpStep++;
            
            //  첫번째 점프 100%
            if (_jumpStep == 0)
            {
                rb2D.AddForce(Vector2.up * _jumpSpeed);
            }
            //  두번째 점프 80%
            else if (_jumpStep == 1)
            {
                rb2D.AddForce(Vector2.up * (_jumpSpeed * 0.8f));
            }
        }
    }
}