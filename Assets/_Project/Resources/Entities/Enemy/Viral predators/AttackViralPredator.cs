using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackViralPredator : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushAway(Transform target, float pushPower)
    {
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction.normalized * pushPower, ForceMode2D.Impulse);
    }
}
