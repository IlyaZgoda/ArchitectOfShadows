using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Attack : MonoBehaviour
{
    public Transform tr;

    Vector3 mousePosition;
    float angle;

    private void Start()
    {
        tr = transform;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);                                        // ”гол между объектами
        angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);                                   //угол между вектором от объекта к мыше и осью х
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);       // ћгновенное вращение
    }

    // функци€ возвращает ближайший объект из массива, относительно указанной позиции
    static GameObject NearTarget(Vector3 position, Collider2D[] array)
    {
        Collider2D current = null;
        float dist = Mathf.Infinity;

        foreach (Collider2D coll in array)
        {
            float curDist = Vector3.Distance(position, coll.transform.position);

            if (curDist < dist && coll.gameObject.tag == "Enemy")
            {
                current = coll;
                dist = curDist;
            }
        }

        return (current != null) ? current.gameObject : null;
    }

    public static void Action(Vector2 point, float radius, float damage, bool allTargets)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius);

        if (!allTargets)
        {
            GameObject obj = NearTarget(point, colliders);
            if (obj != null && obj.CompareTag("Enemy"))
            {
                var enemy = obj.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage((int)damage);
                if (enemy.HealthPoint <= 0) enemy.Die();
            }
            return;
        }

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Enemy>() && hit.CompareTag("Enemy"))
            {
                var enemy = hit.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage((int)damage);
                if (enemy.HealthPoint <= 0) enemy.Die();
            }
        }
    }
}
