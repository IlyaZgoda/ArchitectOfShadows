using Code.Services.InteractionService;
using Code.Services.Windows.Factories;
using Code.StaticData.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Interaction
{
    public class BaseInteractor : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public InteractionConfig Data;
        private IWindowFactory<BaseInteractor> _windowFactory;

        [Inject]
        public void Construct(InteractionWindowFactory windowFactory) => 
            _windowFactory = windowFactory;

        public void Interact()
        {
            var new_transform = transform;
            new_transform.localPosition -= Vector3.up;
            _windowFactory.CreateWindow(new_transform, this);       
        }
    }
}
