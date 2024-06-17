using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreFloorDetector : MonoBehaviour
{
    public bool activated = false;

    private Player _player;

    private bool offPlatform;

    private void Awake()
    {
        _player = transform.parent.GetComponent<Player>();
        offPlatform = false;
    }

    private void Update()
    {
        if (!activated) return;

        if(offPlatform)
        {
            if (_player.inDash == false)
            {
                _player.FallInVoid();
                activated = false;
                offPlatform = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated) return;
        if (collision.tag == "CorePlatform")
        {
            offPlatform = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!activated) return;
        if(collision.tag == "CorePlatform")
        {
            offPlatform = true;
        }
    }
}
