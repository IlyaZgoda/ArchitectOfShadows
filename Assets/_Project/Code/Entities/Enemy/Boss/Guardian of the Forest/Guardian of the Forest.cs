using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianoftheForest : Enemy
{
    [Header("Aтака волной")]
    public float waveDistance = 6f;
    public float waveCoolDown = 3f;

    [Header("Бросок камня")]
    public float rockSpeed = 10f;
    public float rockDistance = 10f;
    public float rockCoolDown = 3f;
    public GameObject rock;

    [Header("Использование замедления")]
    public float slowZonaDistance = 7f;
    public float slowZonaCoolDown = 15f;
    public GameObject slowZona;

    private List<string> Attacks = new List<string>();
    private List<float> CoolDown = new List<float>();
    private List<float> Distances = new List<float>();
    private List<float> Timer = new List<float>();

    private float waveTimer;
    private float rockTimer;
    private float slowTimer;

    private bool isAttack;
    private bool flag;
    private int maxHP;

    void Awake()
    {
        startSettings();


        _animator = GetComponent<Animator>();

        waveTimer = Time.time;
        rockTimer = Time.time;
        slowTimer = Time.time;
        waitCooldown = Time.time;

        Attacks.Add("Attack");
        Attacks.Add("Rock");
        Attacks.Add("SlowZona");

        CoolDown.Add(waveCoolDown);
        CoolDown.Add(rockCoolDown);
        CoolDown.Add(slowZonaCoolDown);

        Timer.Add(waveTimer);
        Timer.Add(rockTimer);
        Timer.Add(slowTimer);

        Distances.Add(waveDistance);
        Distances.Add(rockDistance);
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

    public void StartRock()
    {
        Rock(target);
    }

    public void StartSlow()
    {
        SlowZona();
    }

    private IEnumerator Attack()
    {
        for (int i = 0; i < Attacks.Count; i++)
        {
            if (canAttack(i))
            {
                isAttack = true;
                _animator.SetTrigger(Attacks[i]);
                switch (Attacks[i])
                {
                    case "Rock": StartRock(); break;
                    case "SlowZona": StartSlow(); break;
                }
                Timer[i] = CoolDownTime.Cooldown(CoolDown[i]);
                break;
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
        if (HealthPoint <= (int)maxHP/2)
        {
            CoolDown[0] -= 1;
            CoolDown[1] -= 1;
            CoolDown[2] -= 3;
            flag = false;
        }
    }

    private void Rock(Transform target)
    {
        var obj = Instantiate(rock, transform.localPosition, transform.rotation);
        float angle = Vector3.SignedAngle(Vector3.up, target.position - obj.transform.position, Vector3.forward);
        obj.transform.rotation = Quaternion.Euler(0f, 0f, angle-90);
        obj.transform.GetComponent<Rigidbody2D>().AddForce(direction.normalized * rockSpeed, ForceMode2D.Impulse);
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
