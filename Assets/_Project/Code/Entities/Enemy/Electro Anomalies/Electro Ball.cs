    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBall : Enemy
{
    public GameObject sphere;

    private void Awake()
    {
        Invoke("destroyBall", 3f);
    }

    private void destroyBall()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Health>().TakeDamage((int)Damage);
            Instantiate(sphere, collision.transform.localPosition, collision.transform.rotation);
            Destroy(gameObject);
        }    
    }
}
