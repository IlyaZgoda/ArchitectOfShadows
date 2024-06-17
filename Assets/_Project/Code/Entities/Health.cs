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
        var _damageSoundPrefab = Resources.Load<GameObject>("Prefabs/Sounds/Sound_Damage");
        GameObject sound = Instantiate(_damageSoundPrefab, transform.position, Quaternion.identity);
    }

    public void Die()
    {
        var _deathSoundPrefab = Resources.Load<GameObject>("Prefabs/Sounds/Sound_MonsterDeath");
        GameObject sound = Instantiate(_deathSoundPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
