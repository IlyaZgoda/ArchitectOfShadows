using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ласс чисто дл€ проверки анимаций персонажа
// удалить как будет готова нормальна€ реализаци€
public class PlayerAnimationTest : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveVector;
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");

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

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _animator.SetTrigger("Attack");
        }
    }
}
