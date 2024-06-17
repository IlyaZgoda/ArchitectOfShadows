using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeMoveZone : MonoBehaviour
{
    private Vector2 moveDirection;

    private void Awake()
    {
        moveDirection = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (moveDirection != Vector2.zero) return;
        //Debug.Log("yo");
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<Player>().isDashing == false) return;
            if(collision.transform.position.y < transform.position.y)
            {
                moveDirection = new Vector2(0, 20.5f);
            } 
            else
            {
                moveDirection = new Vector2(0, -20.5f);
            }
            //Debug.Log("movedir is " + moveDirection);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (moveDirection == Vector2.zero) return;
        if (collision.tag == "Player")
        {
            //Debug.Log("staying");
            collision.transform.position = new Vector3(collision.transform.position.x + moveDirection.x * Time.deltaTime, collision.transform.position.y + moveDirection.y * Time.deltaTime, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (moveDirection == Vector2.zero) return;
        if (collision.tag == "Player")
        {
            //Debug.Log("exit");
            moveDirection = Vector2.zero;
        }
    }
}

