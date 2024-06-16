using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FisherMan : Enemy
{
    [Header("Спавн противников")]
    public float spawnDistance = 6f;
    public float spawnCoolDown = 3f;
    public int maxEnemy = 10;
    public GameObject enemy;

    [Header("Кислота")]
    public float acidSpeed = 10f;
    public float acidDistance = 10f;
    public float acidCoolDown = 3f;
    public GameObject acid;

    [Header("Удар по области")]
    public float slowZonaDistance = 7f;
    public float slowZonaCoolDown = 15f;
    public GameObject slowZona;

    private List<string> Attacks = new List<string>();
    private List<float> CoolDown = new List<float>();
    private List<float> Distances = new List<float>();
    private List<float> Timer = new List<float>();

    private float spawnTimer;
    private float acidTimer;
    private float slowTimer;

    private int enemyCount = 0;
    private bool isAttack;
    private bool flag;
    private int maxHP;

    void Awake()
    {
        startSettings();

        _animator = GetComponent<Animator>();

        spawnTimer = Time.time;
        acidTimer = Time.time;
        slowTimer = Time.time;
        waitCooldown = Time.time;

        Attacks.Add("Spawn");
        Attacks.Add("Acid");
        Attacks.Add("SlowZona");

        CoolDown.Add(spawnCoolDown);
        CoolDown.Add(acidCoolDown);
        CoolDown.Add(slowZonaCoolDown);

        Timer.Add(spawnTimer);
        Timer.Add(acidTimer);
        Timer.Add(slowTimer);

        Distances.Add(spawnDistance);
        Distances.Add(acidDistance);
        Distances.Add(slowZonaDistance);

        maxHP = HealthPoint;
        isAttack = false;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        getTarget();

        if (trigger)
        {
            if (flag) bossSecondStage();
            if (!isAttack) StartCoroutine(Attack());

            Move();
            PlayAnimations();
        }
    }

    public void StartSpawn()
    {
        SpawnEnemy(target);
    }

    public void StartAcid()
    {
        Acid(target);
    }

    public void StartSlow()
    {
        SlowZona();
    }

    private IEnumerator Attack()
    {
        if (distance <= damageDistance && Time.time > waitCooldown)
        {
            Debug.Log("Attack");
            waitCooldown = CoolDownTime.Cooldown(1);
        }
        else
        {
            for (int i = 0; i < Attacks.Count; i++)
            {
                if (canAttack(i))
                {
                    isAttack = true;
                    _animator.SetTrigger(Attacks[i]);
                    switch (Attacks[i])
                    {
                        case "Spawn": StartSpawn(); break;
                        case "Acid": StartAcid(); break;
                        case "SlowZona": StartSlow(); break;
                    }
                    Timer[i] = CoolDownTime.Cooldown(CoolDown[i]);
                    break;
                }
            }
        }
        yield return new WaitForSeconds(1);
        isAttack = false;
    }

    private bool canAttack(int i)
    {
        if (Timer[i] < Time.time && Distances[i] >= distance) return true;
        return false;
    }

    private void bossSecondStage()
    {
        if (HealthPoint <= (int)maxHP / 2)
        {
            CoolDown[0] -= 1;
            CoolDown[1] -= 1;
            CoolDown[2] -= 3;
            flag = false;
        }
    }

    private void Acid(Transform target)
    {
        var obj = Instantiate(acid, transform.localPosition, transform.rotation);
        float angle = Vector3.SignedAngle(Vector3.up, target.position - obj.transform.position, Vector3.forward);
        obj.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        obj.transform.GetComponent<Rigidbody2D>().AddForce(direction.normalized * acidSpeed, ForceMode2D.Impulse);
    }

    private void SpawnEnemy(Transform target)
    {
        if (enemyCount < maxEnemy)
        {
            Instantiate(enemy, transform.localPosition, transform.rotation);
            enemyCount += 1;    
        };
    }

    private void SlowZona()
    {
        var obj = Instantiate(slowZona, transform.localPosition, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage((int)Damage);
        }
    }
}
