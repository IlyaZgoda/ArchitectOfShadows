using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CorruptedArchitectStage
{
    Idle,
    Laser1,
    Laser2,
    Laser3,
    Laser4,
    SpawnEnemies
}

public class CorruptedArchitect : MonoBehaviour
{
    [SerializeField] Transform enemiesSpawnPoint1;
    [SerializeField] Transform enemiesSpawnPoint2;
    [SerializeField] Transform enemiesSpawnPoint3;
    [SerializeField] Transform enemiesSpawnPoint4;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;

    public float delayBetweenStages = 5f;
    public int health;
    public CorruptedArchitectStage stage;

    private Animator animator;
    private bool fightStarted = false;

    private float timeBeforeNextStage = 0;

    //private List<Vector3> enemiesSpawnPoints;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = 2;
        stage = CorruptedArchitectStage.Idle;
    }

    public void StartFight()
    {
        //animator.SetTrigger("Idle");
        timeBeforeNextStage = 2f;
        fightStarted = true;

        //enemiesSpawnPoints = new List<Vector3>();
        //enemiesSpawnPoints.Append(enemiesSpawnPoint1.position);
        //enemiesSpawnPoints.Append(enemiesSpawnPoint2.position);
        //enemiesSpawnPoints.Append(enemiesSpawnPoint3.position);
        //enemiesSpawnPoints.Append(enemiesSpawnPoint4.position);
    }

    // режим хаоса, убивает игрока гарантированно
    public void AnnihilatePlayer()
    {
        for (int i = 0; i < 25; i++)
        {
            int spawnPoint = Random.Range(0, 4);
            var e = Instantiate(enemy2, GetSpawnPoint(spawnPoint), Quaternion.identity);
            e.GetComponent<DigitalMag>().Damage = 30;
        }
        fightStarted = true;
        delayBetweenStages = 1f;
    }

    private void Update()
    {
        if (!fightStarted) return;

        timeBeforeNextStage -= Time.deltaTime;

        if(timeBeforeNextStage <= 0f)
        {
            StartNewStage();
            timeBeforeNextStage = delayBetweenStages;
        }
    }

    void StartNewStage()
    {
        stage = (CorruptedArchitectStage)Random.Range(0, 6);
        //Debug.Log(stage);
        if(stage > CorruptedArchitectStage.Idle && stage < CorruptedArchitectStage.SpawnEnemies)
        {
            switch(stage)
            {
                case CorruptedArchitectStage.Laser1:
                    animator.SetTrigger("Attack 1"); break;
                case CorruptedArchitectStage.Laser2:
                    animator.SetTrigger("Attack 2"); break;
                case CorruptedArchitectStage.Laser3:
                    animator.SetTrigger("Attack 3"); break;
                case CorruptedArchitectStage.Laser4:
                    animator.SetTrigger("Attack 4"); break;
            }
            var lazerSound = Resources.Load<GameObject>("Prefabs/Sounds/Sound_SuperLazer");
            GameObject sound = Instantiate(lazerSound, transform.position, Quaternion.identity);
        }
        if(stage == CorruptedArchitectStage.SpawnEnemies)
        {
            int toSpawn = Random.Range(2, 5);
            for(int i = 0; i < toSpawn; i++)
            {
                int spawnPoint = Random.Range(0, 4);
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(enemy1, GetSpawnPoint(spawnPoint), Quaternion.identity);
                } 
                else
                {
                    Instantiate(enemy2, GetSpawnPoint(spawnPoint), Quaternion.identity);
                }
            }
        }
    }

    private Vector3 GetSpawnPoint(int id)
    {
        switch(id)
        {
            case 0:
                return enemiesSpawnPoint1.position;
            case 1:
                return enemiesSpawnPoint2.position;
            case 2:
                return enemiesSpawnPoint3.position;
            case 3:
                return enemiesSpawnPoint4.position;
        }

        return Vector3.zero;
    }

    public void TakeDamage()
    {
        var electricSound = Resources.Load<GameObject>("Prefabs/Sounds/Sound_BossDamage");
        GameObject sound = Instantiate(electricSound, transform.position, Quaternion.identity);
        health -= 1;
        if(health == 1)
        {
            animator.SetTrigger("Hit1");
            timeBeforeNextStage = delayBetweenStages;
            stage = CorruptedArchitectStage.Idle;

        } else if(health == 0)
        {
            animator.SetTrigger("Hit2");
            timeBeforeNextStage = 100f;
            stage = CorruptedArchitectStage.Idle;
            GameObject.Find("Player").GetComponent<Player>().Win();
        }
    }

    public void ResetAll()
    {
        health = 2;
        stage = CorruptedArchitectStage.Idle;
        timeBeforeNextStage = 5f;
        animator.SetTrigger("Idle");
    }
}
