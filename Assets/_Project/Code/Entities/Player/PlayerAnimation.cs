using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ласс чисто дл€ проверки анимаций персонажа
// удалить как будет готова нормальна€ реализаци€
public class PlayerAnimation : MonoBehaviour
{
    static Vector2[] DizzleDirections;

    private Animator _animator;

    private bool _dizzled;
    private float _dizzleTimer;
    private int _dizzleDir;

    void Start()
    {
        _animator = GetComponent<Animator>();

        DizzleDirections = new Vector2[]
        {
            new Vector2(-1, 0),
            new Vector2(0, -1),
            new Vector2(1, 0),
            new Vector2(0, 1)
        };
    }

    void Update()
    {
        Vector2 moveVector;
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");

        if(_dizzled)
        {
            _dizzleTimer += Time.deltaTime;
            if (_dizzleTimer >= 0.15f)
            {
                _animator.SetTrigger("Stand");
                _animator.ResetTrigger("Run");
                _dizzleDir = (_dizzleDir + 1) % 4;
                _animator.SetFloat("Horiz", DizzleDirections[_dizzleDir].x);
                _animator.SetFloat("Vert", DizzleDirections[_dizzleDir].y);
                _dizzleTimer = 0;
            }
            return;
        }

        if (moveVector.magnitude > Vector2.kEpsilon)
        {
            _animator.SetFloat("Horiz", moveVector.x);
            _animator.SetFloat("Vert", moveVector.y);
            _animator.SetTrigger("Run");
            _animator.ResetTrigger("Stand"); //hack
        }
        else // moveVector.magnitude == 0
        {
            _animator.SetTrigger("Stand");
            _animator.ResetTrigger("Run"); //hack
            
        }

    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void Dizzle(bool enable)
    {
        _dizzled = enable;
        _dizzleTimer = 0;
        _dizzleDir = 0;
    }
}
