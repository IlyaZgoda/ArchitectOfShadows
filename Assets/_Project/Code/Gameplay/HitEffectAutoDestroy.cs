using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectAutoDestroy : MonoBehaviour
{
    [SerializeField] float delay = 0f;

    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }

}
