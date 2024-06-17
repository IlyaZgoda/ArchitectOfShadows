using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] AudioClip _clip;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        if( _clip != null )
        {
            SetSound(_clip);
        }
    }

    public void SetSound(AudioClip clip)
    {
        _clip = clip;
        _source.clip = clip;
        _source.pitch = _source.pitch + (Random.Range(0f, 0.4f) - 0.2f);
        _source.Play();
        StartCoroutine(AutoDestroy());
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(_clip.length);
        Destroy(gameObject);
    }
}
