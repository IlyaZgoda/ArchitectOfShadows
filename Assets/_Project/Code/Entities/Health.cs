using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public int HealthPoint = 100;

    public virtual void TakeDamage(int damage)
    {
        HealthPoint -= damage;
        if (HealthPoint <= 0)
        {
            HealthPoint = 0;
        }

        var prefab = Resources.Load<GameObject>("Prefabs/HitEffect");
        GameObject effect = Object.Instantiate(prefab, transform);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
