using Code.Gameplay.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCollider : MonoBehaviour
{
    private bool _visited = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_visited) return;
        if(collision.tag == "Player")
        {
            GetComponentInChildren<NPCInteractor>().Interact();
            GetComponentInChildren<NPCInteractor>().transform.position = collision.transform.position - new Vector3(2, 2);
            _visited = true;
        }
    }
}
