using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] bool spawnAtAwake = true;

    private void Awake()
    {
        Debug.Assert(enemyPrefab != null);

        GetComponent<SpriteRenderer>().enabled = false;

        if(spawnAtAwake) SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        var spawned = GameObject.Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        spawned.transform.position = transform.position;
    }

}
