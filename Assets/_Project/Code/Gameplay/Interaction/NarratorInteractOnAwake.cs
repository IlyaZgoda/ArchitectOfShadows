using Code.Gameplay.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorInteractOnAwake : MonoBehaviour
{
    [SerializeField] bool shouldAppearAtPlayer = false;
    [SerializeField] Animator narratorAnimator;

    private IEnumerator HideNarrator()
    {
        yield return new WaitForSeconds(6f);
        narratorAnimator.SetTrigger("Disappear");
    }

    private void Awake()
    {
        if (shouldAppearAtPlayer)
        {
            transform.position = GameObject.Find("Player").transform.position - new Vector3(3, -3);
        }
        GetComponent<NPCInteractor>().Interact();

        if(narratorAnimator != null)
        {
            narratorAnimator.transform.parent.transform.position = transform.position - new Vector3(0, 6, 0);
            narratorAnimator.SetTrigger("Appear");
            StartCoroutine(HideNarrator());
        }
    }
}
