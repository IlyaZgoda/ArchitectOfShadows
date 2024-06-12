using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float Speed = 5;
    public float Damage = 10f;
    public float RadiusAttack = 0.63f;
    public float attackCooldown = 1;
    public float blinkCooldown = 1;
    public float dashCooldown = 1;
    public float dashSpeed = 5;
    public bool Splash = false;

    private TrailRenderer trailRenderer;
    private bool isDashing = false;

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

    // Interaction
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
         
        }
        if (Time.time > wait.waitAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack.Action(transform.gameObject.GetComponentInChildren<Attack>().GetComponentInChildren<CircleCollider2D>().transform.position, RadiusAttack, Damage, Splash);
                wait.waitAttack = CoolDownTime.Cooldown(attackCooldown);
            }
        }
        if (Time.time > wait.waitDash)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
                wait.waitDash = CoolDownTime.Cooldown(dashCooldown);
            }
        }
    }
    // Movement
    void FixedUpdate()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + moveVector * Speed * Time.deltaTime);
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            Speed *= dashSpeed;
            OffCollision();
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
        OnCollision();
        isDashing = false;
    }

    private void OnCollision()
    {
        capsule.enabled = true;
    }

    private void OffCollision()
    {
        capsule.enabled = false;
    }
}
