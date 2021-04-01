using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Transform Tf => this.transform;

    private Rigidbody2D rb2D;

    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _jumpSpeed = 10;


    private Vector2 pos;

    private void Awake()
    {
        if (rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();

        pos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!IsStopWell(Vector2.left))
            {
                pos.y = Tf.position.y;
                MoveVec(Vector2.left);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!IsStopWell(Vector2.right))
            {
                pos.y = Tf.position.y;
                MoveVec(Vector2.right);
            }
        }
    }

    private void MoveVec(Vector2 vec)
    {
        // Tf.Translate(vec * (_moveSpeed * Time.deltaTime));
        pos += vec * (_moveSpeed * Time.deltaTime);
        Tf.localPosition = pos;
    }

    private bool IsStopWell(Vector2 dir)
    {
        var ridus = Tf.localScale.x / 1.5f;
        var cols = Physics2D.RaycastAll(Tf.position, dir, ridus);

        return cols != null && cols.Length >= 2;
    }
}