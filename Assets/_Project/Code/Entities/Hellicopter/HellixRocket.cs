using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellixRocket : MonoBehaviour
{
    public float Damage;
    public float Speedrocket;

    private Player player;
    private Animator _animator;
    private bool following = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        following = true;
    }

    private void Update()
    {
        if (following)
        {
            float angle = Vector3.SignedAngle(Vector3.up, player.transform.position - transform.position, Vector3.forward);
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
            float step = Speedrocket * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

        }
    }

    public void DeleteRocket()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage((int)Damage);
            following = false;
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            _animator.SetTrigger("Boom");
        }
    }
}
