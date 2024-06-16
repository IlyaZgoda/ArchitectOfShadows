using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellixBullet : MonoBehaviour
{
    public float Damage;
    public float bulletTime;


    private void Awake()
    {
        Invoke("DeleteBullet", bulletTime);
    }

    public void DeleteBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage((int)Damage);
            Debug.Log(1);
        }
    }
}
