using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    public float Damage = 5f;            // Урон
    public float Speed = 10f;            // Скорость
    public float damageDistance = 2f;    // Дистанция для удара
    public float stopDistance = 1.5f;    // Дистанция для остановки противника
    public float coolDown = 2f;          // Задержка перед каждым ударом

    public NavMeshAgent agent;
    public Transform target;

    public float waitCooldown;
    public float distance;
    public Vector3 direction;
    public bool trigger;

    public Animator _animator;

    public void getTarget()
    {
        var triggerArea = transform.gameObject.GetComponentInChildren<TriggerArea>();

        trigger = triggerArea.isTriggered;
        target = triggerArea.target;
        distance = (target.position - transform.position).magnitude;
        direction = target.position - transform.position;
    }

    public void startSettings()
    {
        waitCooldown = Time.time;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Speed;
        agent.stoppingDistance = stopDistance;
    }

    public void Move()
    {
        agent.SetDestination(target.position);
    }

    public void PlayAnimations()
    {
        Vector2 moveVector;
        moveVector.x = agent.velocity.x;
        moveVector.y = agent.velocity.y;

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
}
