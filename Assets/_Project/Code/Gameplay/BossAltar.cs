using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAltar : Health
{
    [SerializeField] CorruptedArchitect corruptedArchitect;
    [SerializeField] float chargeTime = 30f;

    private bool _charged;
    private float _chargeTimer;

    private Animator _animator;

    private void Awake()
    {
        _charged = false;
        _animator = GetComponentInChildren<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("got damage");
        if(_charged)
        {
            corruptedArchitect.TakeDamage();
            _charged = false;
            _chargeTimer = 0;
            _animator.SetBool("charged", _charged);
        }
    }

    private void Update()
    {
        //if (!_charged)
        //{
        _chargeTimer += Time.deltaTime;
        _animator.SetBool("charged", _charged);

        if (_chargeTimer >= chargeTime)
        {
            _charged = true;
        }
        //}
    }

    public void ResetAll()
    {
        _charged = false;
        _chargeTimer = 0;
        _animator.SetBool("charged", _charged);

    }
}
