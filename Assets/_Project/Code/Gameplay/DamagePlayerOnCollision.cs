using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnCollision : MonoBehaviour
{
    [SerializeField] int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<Player>().inDash)
            {
                collision.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}
