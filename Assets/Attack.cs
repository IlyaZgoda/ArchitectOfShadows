using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    private GameObject enemy;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Play");
            Debug.Log(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") AttackEntity(collision.gameObject);
    }
    private void AttackEntity(GameObject entity)
    {
        Debug.Log(entity.gameObject.tag);
        Debug.Log(entity.GetComponent<Enemy>().Health); 
        entity.GetComponent<Enemy>().Health -= (int)GameObject.Find("Player").GetComponent<Player>().Damage;
        if (entity.GetComponent<Enemy>().Health <= 0)
        {
            entity.GetComponent<Enemy>().Health = 0;
            Destroy(entity);
        }
    }
}
