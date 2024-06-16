using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int _healthPoint = 100;

    public virtual int HealthPoint { get; protected set; }

    public virtual void TakeDamage(int damage)
    {
        HealthPoint -= damage;
        if (HealthPoint <= 0)
        {
            HealthPoint = 0;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
