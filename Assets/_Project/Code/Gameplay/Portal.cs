using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform teleportPoint;
    [SerializeField] bool corePortal = true;
    [SerializeField] bool disableAfterTeleport = true;

    private void Awake()
    {
        Debug.Assert(teleportPoint != null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject dog = GameObject.Find("Dog");
            if (!corePortal)
            {
                if (dog)
                {
                    dog.transform.position = teleportPoint.position - new Vector3(1, 0, 0);
                    dog.GetComponent<DogFollowing>().SetAttractor(null);
                }
            } 
            else
            {
                if(dog != null) dog.SetActive(false);
                collision.GetComponentInChildren<CoreFloorDetector>().activated = true;
            }
            collision.transform.position = teleportPoint.position;
            if(disableAfterTeleport) gameObject.SetActive(false);
        }
    }
}
