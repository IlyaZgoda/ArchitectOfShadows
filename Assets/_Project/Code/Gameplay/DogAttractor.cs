using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class DogAttractor : MonoBehaviour
{
    [SerializeField] DogFollowing dog;

    private void Awake()
    {
        Debug.Assert(dog != null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dog.SetAttractor(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dog.SetAttractor(null);
        }
    }
}
