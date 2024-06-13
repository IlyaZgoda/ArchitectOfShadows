using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    public float Damage = 5f;            // ����
    public float Speed = 10f;            // ��������
    public float damageDistance = 2f;    // ��������� ��� �����
    public float stopDistance = 1.5f;    // ��������� ��� ��������� ����������
    public float coolDown = 2f;          // �������� ����� ������ ������

    public NavMeshAgent agent;
    public Transform target;

    public float waitCooldown;
    public float distance;
    public Vector3 direction;
    public bool trigger;

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
}