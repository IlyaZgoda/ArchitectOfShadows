using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float damage = 10f;
    public float timer = 2f;

    public void Awake()
    {
        Invoke("OnDestroy", timer);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}
