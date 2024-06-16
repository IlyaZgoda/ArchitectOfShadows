using Code.Gameplay.Interaction;
using Code.Services.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorInteractOnAwake : MonoBehaviour
{
    [SerializeField] bool shouldAppearAtPlayer = false;
    [SerializeField] Animator narratorAnimator;

    private IWindow w;

    private void HideNarrator()
    {
        narratorAnimator.SetTrigger("Disappear");
    }

    private void Awake()
    {
        Player p = GameObject.Find("Player").GetComponent<Player>();
        if (shouldAppearAtPlayer)
        {
            transform.position = p.transform.position - new Vector3(3, -3);    
        }

        w = GetComponent<NPCInteractor>().Interact(HideNarrator);
        p.SetWindowDestroyWhenPlayerFarAway(w);

        if(narratorAnimator != null)
        {
            narratorAnimator.transform.parent.transform.position = transform.position - new Vector3(0, 6, 0);
            narratorAnimator.SetTrigger("Appear");
            //StartCoroutine(HideNarrator());
        }
    }
}
