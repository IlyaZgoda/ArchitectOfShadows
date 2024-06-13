using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTag : Enemy
{
    public void Start()
    {
        Invoke("deleteLaser", 2f);
    }
    private void deleteLaser()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Player>().TakeDamage((int)Damage);
        }
    }
}
