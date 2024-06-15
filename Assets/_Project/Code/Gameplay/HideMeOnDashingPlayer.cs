using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMeOnDashingPlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.GetComponent<Player>().isDashing)
            {
                transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                transform.parent.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.parent.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
