using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDashOnEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        return;
        if (collision.tag == "Player")
        {
            collision.transform.position = Vector2.Lerp(collision.transform.position + new Vector3(0, 1f), transform.position, 0.25f);
            collision.GetComponent<Player>().isDashing = false;
        }
    }
}
