using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public List<Sprite> sprites;

    public float damage = 10f;
    public float timer = 2f;

    private SpriteRenderer render;

    public void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        render.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
        
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        Rigidbody2D _rb = gameObject.AddComponent<Rigidbody2D>();
        
        _rb.gravityScale = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        Invoke("OnDestroy", timer);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}
