using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public float Damage = 10f;
    public float coolDown = 1.5f;
    public float Distance = 1.5f;
    public float time = 5f;

    public Sprite acidZona;

    private SpriteRenderer _render;
    private Rigidbody2D _rb;

    private bool isPlaced = false;
    private float timer;
    private float timerDamage;

    private void Awake()
    {
        _render = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();

        timer = Time.time;
        timerDamage = Time.time;

        Distance = (float)UnityEngine.Random.Range(0, Distance);

    }

    private void Update()
    {
        if (Time.time > timer + Distance)
        {
            _rb.velocity = Vector2.zero;
            isPlaced = true;
            if (isPlaced)
            {
                _render.sprite = acidZona;
                timer = Time.time;
                Invoke("DestroyAcid", time);
            }
        }
    }

    public void DestroyAcid()
    {
        Destroy(transform.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time <= timerDamage + coolDown) return;
        if (collision.CompareTag("Player"))
        {
            if (isPlaced)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage((int)Damage);

                timerDamage = Time.time;
            }
        }
    }
}
