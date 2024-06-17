using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProcessorBoogies : Enemy
{
    public Transform target1;
    private Rigidbody2D rb;
    private float distance;
    private bool trigger = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();


        startSettings();
    }

    // Update is called once per frame
    void Update()
    {

        distance = (target1.position - transform.position).magnitude;
        if (distance <= 15f) trigger = true;

        if (distance <= damageDistance && Time.time > waitCooldown && trigger)
        {
            _animator.SetTrigger("Attack");
        }


        if (trigger)
        {
            Move();
            PlayAnimations();
        }
    }

    public void StartAttack()
    {
        Debug.Log(1);
        waitCooldown = CoolDownTime.Cooldown(coolDown);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) { collision.GetComponent<Player>().TakeDamage((int)Damage); }
    }
}
