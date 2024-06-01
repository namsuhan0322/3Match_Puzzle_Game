using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // 적의 이동 속도입니다.
    private Rigidbody2D rb;

    private Animator _animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // 왼쪽으로 움직이기 위해 속도를 설정합니다.
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // 계속해서 왼쪽으로 움직이게 합니다.

        if (rb.velocity.y == 0)
        {
            _animator.SetBool("isMove", false);
        }
        else
        {
            _animator.SetBool("isMove", true);
        }
    }
}
