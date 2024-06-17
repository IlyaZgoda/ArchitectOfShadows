using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnCollision : MonoBehaviour
{
    [SerializeField] List<GameObject> toDeactivate;   // deactivate these objects after interact
    [SerializeField] List<GameObject> toActivate;     // activate these objects after interact

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            foreach (var obj in toDeactivate)
            {
                obj.SetActive(false);
            }
            foreach (var obj in toActivate)
            {
                obj.SetActive(true);
            }
            gameObject.SetActive(false);
        }
        
    }
}
