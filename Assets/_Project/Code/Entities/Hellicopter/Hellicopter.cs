using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Hellicopter : MonoBehaviour
{
    public bool flyIN = false;
    public bool flyOUT = false;

    public float Speed = 10f;

    public Transform IN;
    public Transform OUT;

    public GameObject bomb1Pos;
    public GameObject bomb2Pos;
    public Transform target;

    private bool isAttack = false;
    private Animator _animator;
    // Start is called before the first frame update

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        flyIN = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flyIN)
        {
            PointFollowing(IN);
            CheckPos(IN);
        }
        if (flyOUT)
        {
            PointFollowing(OUT);
            CheckPos(OUT);
        }
        if (isAttack)
        {
            StartCoroutine(BoomBoom());
        }
    }

    public void PointFollowing(Transform target)
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public void Bomb()
    {
        bomb1Pos.SetActive(true);
        bomb2Pos.SetActive(true);
    }

    public IEnumerator BoomBoom()
    {
        Bomb();
        isAttack = false;
        yield return new WaitForSeconds(4f);
        flyOUT = true;
    }

    public void CheckPos(Transform target)
    {
        if (transform.position == target.position)
        {
            if(flyIN)
            {
                flyIN = false;
                flyOUT = false;
                isAttack = true;
                }
        }
    }
}
