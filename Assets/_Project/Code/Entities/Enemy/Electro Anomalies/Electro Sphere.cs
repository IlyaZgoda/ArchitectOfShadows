using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectroSphere : MonoBehaviour
{
    public float sphereDamage = 5f;
    public float damageTime = 1.5f;
    public float time = 3f;
    public bool trap = true;

    private float timer;

    private void Awake()
    {
        timer = Time.time;
        if (trap)
        {
            Invoke("destroySphere", time); 
        }
    }

    public void destroySphere()
    { Destroy(gameObject); }    

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (Time.time - timer < damageTime) return;
        if (coll.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<Player>().TakeDamage((int)sphereDamage);
            timer = Time.time;
            
        }
    }
}
