using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Hellicopter : MonoBehaviour
{
    public bool flyIN = false;
    public bool flyOUT = false;

    private Animator _animator;
    // Start is called before the first frame update

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flyIN) FIN();
        if (flyOUT) FOUT();        
    }

    public void FIN()
    {
        _animator.SetTrigger("FlyIN");
        flyIN = false;
    }

    public void FOUT()
    {
        _animator.SetTrigger("FlyOUT");
        flyOUT = false;
    }
}
