using Code.Gameplay.Interaction;
using Code.Services.InteractionService;
using Code.Services.Windows.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("interact");
        IInteractable target;

        if (collision.TryGetComponent(out target))
        {
            Debug.Log("interact");
            target.Interact();
        }
    }

}
