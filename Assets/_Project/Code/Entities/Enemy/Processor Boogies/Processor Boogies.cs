using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProcessorBoogies : Enemy
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        startSettings();
    }

    // Update is called once per frame
    void Update()
    {
        getTarget();

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
