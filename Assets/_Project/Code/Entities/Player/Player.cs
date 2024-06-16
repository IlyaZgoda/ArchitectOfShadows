using Code.Gameplay.Interaction.Dialogues;
using Code.Services.InteractionService;
using Code.Services.Windows;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// КОДИРОВКА ПОЛЕТЕЛА У КИРИЛЛИЦЫ КОГДА КОНФЛИКТЫ МЕРДЖА РЕШАЛ
// ПИЗДЕЦ БЛТЯТЬ!!!! НУ ПОХУЙ КТО ЭТИ КОММЕНТЫ ЧИТАЕТ ТО)))

public class Player : Health
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator weaponEffect;
    public float Speed = 5;             // �������� ������
    public float Damage = 10f;          // �����
    public float RadiusAttack = 0.63f;  // ������ ������� �����
    public float attackCooldown = 1;    // ����������� �����
    public float blinkCooldown = 1;     // ����������� ���������
    public float dashCooldown = 1;      // ����������� ����
    public float dashSpeed = 5;         // �������� ����
    public float blockingDamage = 1f;   // ����� ���������� ����� ��� ������������� ���� ��� ��������� �����
    public float closeWindowDistance = 4f;   // ��������� �� ������� �������� ���� �������������� �����������
    public bool Splash = false;         // ���� �� �������
    public bool isDashing = false;      // �������� �� ��������� ����
    public bool inDash = false;         // ��������� �� ����� ��������������� � ����
    public bool immortal = false;       // ����������������� �����
    public bool isFallingToVoid = false;// ������ � ������� (� ����)
    public bool isControllable = true;  // ����������
    public bool dashEnabled = false;    // ������� �� ���
    public bool fishingEnabled = false; // �����������
    public bool deathIsGameOver = false;
    public CoreRestorer coreRestorer;
    
    public Transform weapon;

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


    private IInteractable nearestInteractable;  // ������ ��������� ���������� � �������� ������������
    private GameObject nearestInteractableObj;     // �����(((((((((( �� � ���� ������ ��� �� ���������� ��� ��� �� ������ ����, �� ��� ����� �� �������!!!!
    private IWindow currentOpenWindow;
    private Vector2 pointWhereDialogStarted;

    private AudioSource audioSource;

    private PlayerAnimation playerAnimation;

    private float fallingToVoidTimer;
    private float fallingSpeed;
    private int initialSpriteRendererLayer;

    private Vector2 savePoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        trailRenderer = GetComponent<TrailRenderer>();

        CoolDown wait = new CoolDown();
        wait.waitAttack = Time.time;
        wait.waitBlink = Time.time;
        wait.waitDash = Time.time;

        trailRenderer = GetComponent<TrailRenderer>();

        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Debug.Assert(spriteRenderer != null);
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponentInChildren<AudioSource>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        initialSpriteRendererLayer = spriteRenderer.sortingOrder;

        savePoint = transform.position;
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
                if(deathIsGameOver)
                {
                    GameOver();
                    isFallingToVoid = false;
                    return;
                }
                isFallingToVoid = false;
                ReviveInCore();
            }

            return;
        }

        if (isControllable == false) return;

        // �������������� � interactable
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentOpenWindow == null && nearestInteractable != null)
            {
                if (nearestInteractableObj.name == "TA_Dialog")     // �������� � ������������, ���� ������
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(EnableDash));
                }
                else if (nearestInteractableObj.name == "OW_FinalDialog")   // �������� � ��������, ��������� ������ � ����
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(OpenPortalToCore));
                }
                else if (nearestInteractableObj.name == "SaveInteract")     // "��������" � ������
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(UpdateSavePoint));   
                }
                else if (nearestInteractableObj.name == "F_AfterKey")       // �������� � �������, ���� ������
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(EnableFishing));
                }
                else
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact());
                }
            }
        }
        // ���������� �����
        if (Time.time > wait.waitAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerAnimation.Attack();
                Attack.Action(weapon.position, RadiusAttack, Damage, Splash, weaponEffect);
                wait.waitAttack = CoolDownTime.Cooldown(attackCooldown);
            }
        }

        if (dashEnabled)
        {
            // ���������� ����
            if (Time.time > wait.waitDash)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Dash();
                    wait.waitDash = CoolDownTime.Cooldown(dashCooldown);
                }
            }
        }

        // ���
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Assert(audioSource != null);
            audioSource.Play();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if (transform.position.y > 100f)
            {
                ReviveInCore();
            }
            else
            {
                Respawn();
            }
        }
    }

    // ����������� ������
    void FixedUpdate()
    {
        if(isControllable == false) return;

        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + moveVector * Speed * Time.deltaTime);

        if(currentOpenWindow != null)
        {
            //Debug.Log(currentOpenWindow.IsStillExist());
            if (!currentOpenWindow.IsStillExist())
            {
                currentOpenWindow = null;
                return;
            }
            if (transform.position.y < 100.0f) // ���� �� � ���� - ��������� ������ ����� ������� ������
            {
                if (Vector2.Distance(transform.position, pointWhereDialogStarted) >= closeWindowDistance)
                {
                    currentOpenWindow.Destroy();
                    currentOpenWindow = null;
                }
            } // � ���� �� ��������� ������ - ��� ����� ���� ������ � ������������
        }
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
                HealthPoint = 9999;
                Death();
            }
            onImmortal();
         }
    }

    private IEnumerator RespawnDelayed()
    {
        yield return new WaitForSeconds(3f);

        if (deathIsGameOver == false) // возрождаемся
        {
            if (transform.position.y > 100f)
            {
                ReviveInCore();
            }
            else
            {
                Respawn();
            }
        }
        else  // game over
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
    }
    public void Win()
    {
        Debug.Log("WIN");
        // В ИДЕАЛЕ ЗАДЕРЖКУ 3-5 СЕК А ТО ДАЖЕ АНИМАЦИЮ СМЕРТИ БОССА НЕ УВИДИМ
    }

    private void Death()
    {
        isControllable = false;
        playerAnimation.Death();
        StartCoroutine(RespawnDelayed());
    }

    public void onImmortal()
    { 
        immortal = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        Invoke("offImmortal", blockingDamage);
    }
    public void offImmortal()
    {
        immortal = false;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable target;

        if (collision.TryGetComponent(out target))
        {
            nearestInteractable = target;
            nearestInteractableObj = collision.gameObject;
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
        coreRestorer.RestoreCore();

        //transform.position = new Vector3(21, 134.88f); // �� ������� � ���
        Respawn();

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

    public void SetWindowDestroyWhenPlayerFarAway(IWindow w)
    {
        currentOpenWindow = w;
        pointWhereDialogStarted = transform.position;
    }

    public void EnableDash()
    {
        dashEnabled = true;
    }
    private void OpenPortalToCore()
    {
        nearestInteractableObj.GetComponent<PortalToCoreLink>().portalToCore.gameObject.SetActive(true); // �����
    }

    public void UpdateSavePoint()
    {
        savePoint = transform.position;
    }

    private void Respawn()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            if (enemy.name != "FisherMan 1")
                Destroy(enemy);
        }

        var spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<EnemySpawner>().SpawnEnemy();
        }

        transform.position = savePoint;
        HealthPoint = 100;
        isControllable = true;
        playerAnimation.Reborn();
    }

    private void EnableFishing()
    {
        fishingEnabled = true;
    }
}
