using Code.Services.InteractionService;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Health
{
    public float Speed = 5;             // �������� ������
    public float Damage = 10f;          // �����
    public float RadiusAttack = 0.63f;  // ������ ������� �����
    public float attackCooldown = 1;    // ����������� �����
    public float blinkCooldown = 1;     // ����������� ���������
    public float dashCooldown = 1;      // ����������� ����
    public float dashSpeed = 5;         // �������� ����
    public float blockingDamage = 1f;   // ����� ���������� ����� ��� ������������� ���� ��� ��������� �����
    public bool Splash = false;         // ���� �� �������
    public bool isDashing = false;      // �������� �� ��������� ����
    public bool inDash = false;         // ��������� �� ����� ��������������� � ����
    public bool immortal = false;       // ����������������� �����
    public bool isFallingToVoid = false;// ������ � ������� (� ����)
    public bool isControllable = true;  // ����������
    

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

    private SpriteRenderer spriteRenderer;

    private IInteractable nearestInteractable; // ������ ��������� ���������� � �������� ������������

    private AudioSource audioSource;

    private PlayerAnimationTest playerAnimation;

    private float fallingToVoidTimer;
    private float fallingSpeed;
    private int initialSpriteRendererLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();

        CoolDown wait = new CoolDown();
        wait.waitAttack = Time.time;
        wait.waitBlink = Time.time;
        wait.waitDash = Time.time;

        trailRenderer = GetComponent<TrailRenderer>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponentInChildren<AudioSource>();
        playerAnimation = GetComponentInChildren<PlayerAnimationTest>();
        initialSpriteRendererLayer = spriteRenderer.sortingOrder;
    }

    private void Update()
    {
        if (isFallingToVoid)
        {
            fallingToVoidTimer += Time.deltaTime * 2f;
            fallingSpeed += fallingToVoidTimer * Time.deltaTime * 7f;
            spriteRenderer.transform.localPosition += new Vector3(0, -fallingSpeed) * Time.deltaTime;
            spriteRenderer.transform.localScale = new Vector3(1 - fallingToVoidTimer * 0.1f, 1 - fallingToVoidTimer * 0.1f);
            spriteRenderer.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -fallingToVoidTimer * 4.0f);

            var clr = spriteRenderer.color;
            clr.a = 1.0f - fallingToVoidTimer * 0.75f;
            spriteRenderer.color = clr;

            if(fallingToVoidTimer >= 3f)
            {
                isFallingToVoid = false;
                ReviveInCore();
            }

            return;
        }

        if (isControllable == false) return;

        // �������������� � interactable
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(nearestInteractable != null)
            {
                nearestInteractable.Interact();
            }
        }
        // ���������� �����
        if (Time.time > wait.waitAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack.Action(transform.gameObject.GetComponentInChildren<Attack>().GetComponentInChildren<CircleCollider2D>().transform.position, RadiusAttack, Damage, Splash);
                wait.waitAttack = CoolDownTime.Cooldown(attackCooldown);
            }
        }
        // ���������� ����
        if (Time.time > wait.waitDash)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
                wait.waitDash = CoolDownTime.Cooldown(dashCooldown);
            }
        }

        // ���
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Assert(audioSource != null);
            audioSource.Play();
        }
    }

    // ����������� ������
    void FixedUpdate()
    {
        if(isControllable == false) return;

        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + moveVector * Speed * Time.deltaTime);
    }

    // ������� ��������� � ����������� ����
    private void Dash()
    {
        if (!isDashing)
        {
            immortal = true;
            isDashing = true;
            inDash = true;
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
        inDash = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable target;

        if (collision.TryGetComponent(out target))
        {
            nearestInteractable = target;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable target;

        if (collision.TryGetComponent(out target))
        {
            if(nearestInteractable == target)
                nearestInteractable = null;
        }
    }

    // ��� ������� � ������� � ����
    public void FallInVoid()
    {
        isFallingToVoid = true;
        fallingToVoidTimer = 0.0f;
        fallingSpeed = 7.0f;

        isControllable = false;
        spriteRenderer.sortingOrder = 3;
        playerAnimation.Dizzle(true);
    }

    private void ReviveInCore()
    {
        GameObject.Find("CoreBackground").GetComponent<CoreRestorer>().RestoreCore();

        transform.position = new Vector3(21, 134.88f); // �� ������� � ���

        var clr = spriteRenderer.color;
        clr.a = 1.0f;
        spriteRenderer.color = clr;

        spriteRenderer.transform.localPosition = Vector3.zero;
        spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
        spriteRenderer.transform.localRotation = Quaternion.identity;

        GetComponentInChildren<CoreFloorDetector>().activated = true;

        isControllable = true;
        spriteRenderer.sortingOrder = initialSpriteRendererLayer;
        playerAnimation.Dizzle(false);
    }
}
