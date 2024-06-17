using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DigitalMag : Enemy
{
    public GameObject Laser;
    public float laserSpeed = 10f;

    private Rigidbody2D rb;
    private Transform SpawnPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = rb.GetComponent<Animator>();

        SpawnPoint = transform.GetChild(1);

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
        Attack(target);
        waitCooldown = CoolDownTime.Cooldown(coolDown);
    }

    private void Attack(Transform target)
    {
        var obj = Instantiate(Laser, SpawnPoint);
        float angle = Vector3.SignedAngle(Vector3.up, target.position - obj.transform.position, Vector3.forward);
        obj.transform.rotation = Quaternion.Euler(0f, 0f, angle+90);
        obj.transform.GetComponent<Rigidbody2D>().AddForce(direction.normalized * laserSpeed, ForceMode2D.Impulse);
        obj.transform.localScale /= 3;
        obj.GetComponent<LaserTag>().Damage = Damage;

        var electricSound = Resources.Load<GameObject>("Prefabs/Sounds/Sound_Shoot");
        GameObject sound = Instantiate(electricSound, transform.position, Quaternion.identity);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target.GetComponent<Player>().TakeDamage((int)Damage);
        }
    }

}