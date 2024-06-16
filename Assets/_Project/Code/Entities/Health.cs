using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        Debug.Log("hit " + gameObject.name);
        var prefab = Resources.Load<GameObject>("Prefabs/HitEffect");
        GameObject effect = Object.Instantiate(prefab, transform);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
