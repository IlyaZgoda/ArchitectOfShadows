using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class CorePlatformDisappear : MonoBehaviour
{
    [SerializeField] float fallingAccelerationSpeed = 1f;

    private Vector2 _originPos;

    private bool _playerOnPlatform = false;
    private bool _falling = false;

    private float _fallingSpeed = 0.0f;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originPos = transform.position;
    }

    private void Update()
    {
        if (!_falling) return;

        _fallingSpeed += Time.deltaTime * fallingAccelerationSpeed;
        transform.position += new Vector3(0, -_fallingSpeed * 2f, 0) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, -_fallingSpeed * 3.0f);

        var clr = _spriteRenderer.color;
        clr.a = 1.0f - _fallingSpeed * 0.25f;
        _spriteRenderer.color = clr;

        if(_fallingSpeed >= 8.0f)
        {
            _falling = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_falling) return;
        if (collision.tag == "Player")
        {
            _playerOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_playerOnPlatform)
            {
                _falling = true;
                _playerOnPlatform = false;
            }
        }
    }

    public void RestoreToOriginPosition()
    {
        transform.position = _originPos;
        transform.localRotation = Quaternion.identity;

        _fallingSpeed = 0.0f;
        _playerOnPlatform = false;
        _falling = false;

        var clr = _spriteRenderer.color;
        clr.a = 1;
        _spriteRenderer.color = clr;

        //Debug.Log(gameObject.name);
    }
}
