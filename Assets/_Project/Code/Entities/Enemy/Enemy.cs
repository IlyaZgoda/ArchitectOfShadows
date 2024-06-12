using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy :MonoBehaviour
{
    public float Damage = 5f;
    public float Speed = 10f;
    public float stopDistance = 1.5f;
    public float coolDown = 2f;

    private NavMeshAgent agent;
    private Transform target;
    private Rigidbody2D rb;

    private float waitCooldown;
    private float distance;
    private bool trigger;

    private AttackViralPredator push;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCooldown = Time.time;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Speed;
        agent.stoppingDistance = stopDistance;

        push = GetComponent<AttackViralPredator>();
    }

    // Update is called once per frame
    void Update()
    {
        var triggerArea = transform.gameObject.GetComponentInChildren<TriggerArea>();

        trigger = triggerArea.isTriggered;
        target = triggerArea.target;
        distance = (target.position - transform.position).magnitude;

        if (distance <= stopDistance && Time.time > waitCooldown) 
        {
            //push.PushAway(target, Damage);
            waitCooldown = CoolDownTime.Cooldown(coolDown);
        }

        if (trigger)
        {
            agent.SetDestination(target.position);
        }
    }

}
