using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: заменить на поиск путей как у врагов
public class DogFollowing : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Animator animator;
    private Transform _attractor;

    // alternative to lerp with correct deltaTime handling
    // https://www.youtube.com/watch?v=LSNQuFEDOyQ&t=2988s
    private float expDecay(float a, float b, float decay, float dt)
    {
        return b + (a - b) * Mathf.Exp(-decay * dt);
    }

    private void Awake()
    {
        Debug.Assert(player != null);
        //Debug.Assert(animator != null);
        animator = GetComponent<Animator>();
        _attractor = player;
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        Vector2 attractor_pos = _attractor.position;

        if (Vector2.Distance(attractor_pos, pos) > 2f)
        {
            animator.SetTrigger("Walk");

            pos.x = expDecay(pos.x, attractor_pos.x, 1, Time.deltaTime);
            pos.y = expDecay(pos.y, attractor_pos.y, 1, Time.deltaTime);

            animator.SetFloat("Horiz", transform.position.x - pos.x );
            animator.SetFloat("Vert", pos.y - transform.position.y);

            transform.position = pos;
        } else
        {
            if(_attractor != player)
            {
                animator.SetTrigger("Sit");
            }
        }
    }

    public void SetAttractor(Transform t)
    {
        if (t == null)
        {
            _attractor = player;
        }
        else
        {
            _attractor = t;
        }
    }
}
