using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 100;
    public float Damage = 10f;
    public float Speed = 10;
    public List<GameObject> Inventory = new List<GameObject>();

    private Vector2 moveVector;
    private Rigidbody2D rb;
    private GameObject Object = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Interaction
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Object != null)
            {
                Inventory.Add(Object);
                for (int i = 0; i < Inventory.Count; i++) 
                {
                    Debug.Log(Inventory[i].tag);
                }
            }
        }   
    }

    // Movement
    void FixedUpdate()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + moveVector * Speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "NPC")
        {
            Object = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Object = null;
    }


   
}
