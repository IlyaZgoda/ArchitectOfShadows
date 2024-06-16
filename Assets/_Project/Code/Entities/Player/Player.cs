using Code.Gameplay.Interaction.Dialogues;
using Code.Services.InteractionService;
using Code.Services.Windows;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Health
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator weaponEffect;
    public float Speed = 5;             // Скорость игрока
    public float Damage = 10f;          // Дамаг
    public float RadiusAttack = 0.63f;  // Радиус области атаки
    public float attackCooldown = 1;    // Перезарядка атаки
    public float blinkCooldown = 1;     // Перезарядка телепорта
    public float dashCooldown = 1;      // Перезарядка дэша
    public float dashSpeed = 5;         // Скорость дэша
    public float blockingDamage = 1f;   // Время блокировки урона при использовании дэша или получения урона
    public float closeWindowDistance = 4f;   // Дистанция на которой открытые окна взаимодействия закрываются
    public bool Splash = false;         // Урон по области
    public bool isDashing = false;      // Проверка на состояние дэша
    public bool inDash = false;         // Находится ли игрок непосредственно в дэше
    public bool immortal = false;       // Невосприимчивость урона
    public bool isFallingToVoid = false;// Падени в пустоту (в ядре)
    public bool isControllable = true;  // Управление
    public bool dashEnabled = false;    // Включен ли дэш
    public bool fishingEnabled = false; // РЫБААЛКАААА
    

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


    private IInteractable nearestInteractable;  // храним последний интерактбл к которому приблизились
    private GameObject nearestInteractableObj;     // плохо(((((((((( но у меня меньше дня на реализацию так что не судите меня, вы мою жизнь не прожили!!!!
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

        CoolDown wait = new CoolDown();
        wait.waitAttack = Time.time;
        wait.waitBlink = Time.time;
        wait.waitDash = Time.time;

        trailRenderer = GetComponent<TrailRenderer>();

        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Debug.Assert(spriteRenderer != null);
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
                isFallingToVoid = false;
                ReviveInCore();
            }

            return;
        }

        if (isControllable == false) return;

        // Взаимодействие с interactable
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentOpenWindow == null && nearestInteractable != null)
            {
                if (nearestInteractableObj.name == "TA_Dialog")     // разговор с архитектором, дает абилки
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(EnableDash));
                }
                else if (nearestInteractableObj.name == "OW_FinalDialog")   // разговор с старухой, открывате портал в ядро
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(OpenPortalToCore));
                }
                else if (nearestInteractableObj.name == "SaveInteract")     // "разговор" с сейвом
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(UpdateSavePoint));   
                }
                else if (nearestInteractableObj.name == "F_AfterKey")       // разговор с рыбаком, дает удочку
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact(EnableFishing));
                }
                else
                {
                    SetWindowDestroyWhenPlayerFarAway(nearestInteractable.Interact());
                }
            }
        }
        // Выполнение атаки
        if (Time.time > wait.waitAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerAnimation.Attack();
                //Attack.Action(transform.position, RadiusAttack, Damage, Splash, weaponEffect);
                Attack.Action(GetComponentInChildren<Attack>().transform.position, RadiusAttack, Damage, Splash, weaponEffect); // temporary
                wait.waitAttack = CoolDownTime.Cooldown(attackCooldown);
            }
        }

        if (dashEnabled)
        {
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

        // КРЯ
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

    // Перемещение игрока
    void FixedUpdate()
    {
        if(isControllable == false) return;

        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + moveVector * Speed * Time.deltaTime);

        if(currentOpenWindow != null)
        {
            Debug.Log(currentOpenWindow.IsStillExist());
            if (!currentOpenWindow.IsStillExist())
            {
                currentOpenWindow = null;
                return;
            }
            if (transform.position.y < 100.0f) // если не в ядре - закрываем окошки когда отходим далеко
            {
                if (Vector2.Distance(transform.position, pointWhereDialogStarted) >= closeWindowDistance)
                {
                    currentOpenWindow.Destroy();
                    currentOpenWindow = null;
                }
            } // в ядре не закрываем окошки - там всего один диалог с архитектором
        }
    }

    // Функции активации и деактивации дэша
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

    // При падении в пустоту в Ядре
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
        GameObject.Find("Core").GetComponent<CoreRestorer>().RestoreCore();

        //transform.position = new Vector3(21, 134.88f); // ДА ХАРДКОД И ЧТО
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
        nearestInteractableObj.GetComponent<PortalToCoreLink>().portalToCore.gameObject.SetActive(true); // ууууу
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
            Destroy(enemy);
        }

        var spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<EnemySpawner>().SpawnEnemy();
        }

        transform.position = savePoint;
        HealthPoint = 100;
    }

    private void EnableFishing()
    {
        fishingEnabled = true;
    }
}
