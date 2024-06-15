using Code.Gameplay.Interaction;
using Code.Services.InteractionService;
using Code.Services.Windows;
using Code.Services.Windows.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    IInteractable target;
    IWindow window;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out target))
        {
            window = target.Interact(() => Debug.Log("callback"));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (window != null)
            window.Destroy();          
    }

}
