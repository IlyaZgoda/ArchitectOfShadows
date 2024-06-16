using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroAnomalies : Enemy
{
    public GameObject electroBall;
    public float ballSpeed = 10f;
    public float coolDownAttack = 1.5f;

    public GameObject electroArea;
    public float areaDamage = 3f;
    public float coolDownArea = 3f;

    private float timer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        startSettings();

        _animator = GetComponent<Animator>();

        waitCooldown = Time.time;
        timer = Time.time + coolDownArea;
    }

    // Update is called once per frame
    private void Update()
    {
        getTarget();

        if (distance <= damageDistance && Time.time > waitCooldown && trigger)
        {
            _animator.SetTrigger("Attack");
        }

        if (distance <= damageDistance && Time.time > timer && trigger)
        {
            Instantiate(electroArea, transform);
            timer = CoolDownTime.Cooldown(coolDownArea*2); ;
        }


        if (trigger)
        {
            PlayAnimations();
            Move();
        }
    }

    public void StartAttack()
    {
        Attack(target);
        waitCooldown = CoolDownTime.Cooldown(coolDown);
    }

    private void Attack(Transform target)
    {
        var obj = Instantiate(electroBall, transform);
        float angle = Vector3.SignedAngle(Vector3.up, target.position - obj.transform.position, Vector3.forward);
        obj.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
        obj.transform.GetComponent<Rigidbody2D>().AddForce(direction.normalized * ballSpeed, ForceMode2D.Impulse);

    }
    private void DestroyArea()
    {
        Destroy(electroArea);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<Player>().immortal)
        {
            target.GetComponent<Player>().TakeDamage((int)areaDamage);
        }
    }
}
