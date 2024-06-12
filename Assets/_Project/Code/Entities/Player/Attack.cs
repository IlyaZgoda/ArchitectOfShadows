using System.Collections;
using System.Collections.Generic;
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

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ���� ����� ���������
        angle = Vector2.Angle(Vector2.right, mousePosition - transform.position); //���� ����� �������� �� ������� � ���� � ���� �

        // ���������� ��������
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);

    }

    // ������� ���������� ��������� ������ �� �������, ������������ ��������� �������
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
            if (obj != null && obj.GetComponent<Health>())
            {
                Health hp = obj.GetComponent<Health>();
                hp.TakeDamage((int)damage);
            }
            return;
        }

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Health>() && hit.CompareTag("Enemy"))
            {
                Health hp = hit.GetComponent<Health>();
                hp.TakeDamage((int)damage);
            }
        }
    }
}
