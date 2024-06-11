using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int Health = 50;
    public float Damage = 5;
    public float coolDown = 2f;
    public GameObject Player;

    private NavMeshAgent agent;
    private Rigidbody2D rb;
    private Time timeAttach;
    private float distance;
    private bool isTriggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (Player.transform.position - transform.position).magnitude;
        if (distance <= 2) { }

        if (isTriggered)
        {
            agent.SetDestination(Player.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") isTriggered = true;
    }

    private void Attack()
    {

    }
}
