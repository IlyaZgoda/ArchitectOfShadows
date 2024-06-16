using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCameraALittleFar : MonoBehaviour
{
    [SerializeField] float targetCameraSize = 17f;
    [SerializeField] float speed = 7f;
    [SerializeField] Transform target;
    [SerializeField] Player player;

    Camera cam;

    // alternative to lerp with correct deltaTime handling
    // https://www.youtube.com/watch?v=LSNQuFEDOyQ&t=2988s
    private float expDecay(float a, float b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }

    private void Awake()
    {
        cam = Camera.main;       
    }

    private void LateUpdate()
    {
        if (player.transform.position.x <= 165f)
        {
            cam.orthographicSize = expDecay(cam.orthographicSize, 10, speed, Time.deltaTime);
            cam.transform.localPosition = new Vector3(0, 0, -10);
            return;
        }

        cam.orthographicSize = expDecay(cam.orthographicSize, targetCameraSize, speed, Time.deltaTime);
        var pos = cam.transform.position;

        pos.x = expDecay(cam.transform.position.x, target.position.x, speed, Time.deltaTime);
        pos.y = expDecay(cam.transform.position.y, target.position.y, speed, Time.deltaTime);
        cam.transform.position = pos;

        if(targetCameraSize - cam.orthographicSize <= 0.005f)
        {
            cam.transform.position = target.position;
            //gameObject.SetActive(false);
        }
    }
}
