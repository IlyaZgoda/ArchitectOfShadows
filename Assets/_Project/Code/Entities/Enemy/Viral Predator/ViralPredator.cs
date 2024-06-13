using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackViralPredator : Enemy
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startSettings();
    }

    // Update is called once per frame
    void Update()
    {
        getTarget();

        if (distance <= damageDistance && Time.time > waitCooldown && trigger)
        {
            Attack(target);
            waitCooldown = CoolDownTime.Cooldown(coolDown);
        }


        if (trigger)
        {
            Move();
        }
    }

    private void Attack(Transform target)
    {
        rb.AddForce(direction.normalized * 100, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage((int)Damage);
        }
    }
}
