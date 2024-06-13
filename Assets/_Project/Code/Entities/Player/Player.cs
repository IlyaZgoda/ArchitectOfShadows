using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Health
{
    public float Speed = 5;             // Скорость игрока
    public float Damage = 10f;          // Дамаг
    public float RadiusAttack = 0.63f;  // Радиус области атаки
    public float attackCooldown = 1;    // Перезарядка атаки
    public float blinkCooldown = 1;     // Перезарядка телепорта
    public float dashCooldown = 1;      // Перезарядка дэша
    public float dashSpeed = 5;         // Скорость дэша
    public float blockingDamage = 1f;   // Время блокировки урона при использовании дэша или получения урона
    public bool Splash = false;         // Урон по области
    public bool isDashing = false;      // Проверка на состояние дэша
    public bool immortal = false;       // Невосприимчивость урона
    

    private TrailRenderer trailRenderer;
    private Vector2 moveVector;
    private struct CoolDown
    {
        public float waitAttack;
        public float waitBlink;
        public float waitDash;
    };
    private CoolDown wait;
    private CapsuleCollider2D capsule;
    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();

        CoolDown wait = new CoolDown();
        wait.waitAttack = Time.time;
        wait.waitBlink = Time.time;
        wait.waitDash = Time.time;

        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
         
        }
        // Выполнение атаки
        if (Time.time > wait.waitAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack.Action(transform.gameObject.GetComponentInChildren<Attack>().GetComponentInChildren<CircleCollider2D>().transform.position, RadiusAttack, Damage, Splash);
                wait.waitAttack = CoolDownTime.Cooldown(attackCooldown);
            }
        }
        // Выполнение дэша
        if (Time.time > wait.waitDash)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
                wait.waitDash = CoolDownTime.Cooldown(dashCooldown);
            }
        }
    }

    // Перемещение игрока
    void FixedUpdate()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + moveVector * Speed * Time.deltaTime);
    }

    // Функции активации и деактивации дэша
    private void Dash()
    {
        if (!isDashing)
        {
            immortal = true;
            isDashing = true;
            Speed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        Speed /= dashSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
        Invoke("offImmortal", blockingDamage);
    }

    public override void TakeDamage(int damage)
    {
        if (!immortal)
        {
            HealthPoint -= damage;
            if (HealthPoint <= 0)
            {
                HealthPoint = 0;
            }
            onImmortal();
         }
    }

    public void onImmortal()
    { 
        immortal = true;
        Invoke("offImmortal", blockingDamage);
    }
    public void offImmortal()
    { immortal = false; }
}
