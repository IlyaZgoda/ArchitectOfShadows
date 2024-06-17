using Code.Gameplay.Interaction;
using Code.Services.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorInteractOnAwake : MonoBehaviour
{
    [SerializeField] bool shouldAppearAtPlayer = false;
    [SerializeField] bool spawnFisherman = false;
    [SerializeField] bool makeDogSpawnPortal = false;
    [SerializeField] GameObject portalToCore;
    [SerializeField] DogFollowing dog;
    [SerializeField] GameObject fisherman;
    [SerializeField] Animator narratorAnimator;

    private IWindow w;

    private void HideNarrator()
    {
        narratorAnimator.SetTrigger("Disappear");
        if(spawnFisherman)
        {
            fisherman.SetActive(true);
            if(!makeDogSpawnPortal)
            {
                dog.gameObject.SetActive(false);
                GameObject.Find("Player").GetComponent<Player>().deathIsGameOver = true;
                //fisherman.GetComponent<FisherMan>().HealthPoint = 1000;
                fisherman.GetComponent<FisherMan>().Damage = 40;
            }
        }
        if(makeDogSpawnPortal)
        {
            portalToCore.gameObject.SetActive(true);
            dog.SetAttractor(portalToCore.transform);
        }
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
