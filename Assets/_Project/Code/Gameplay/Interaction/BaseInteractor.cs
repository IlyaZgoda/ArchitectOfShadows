using Code.Services.InteractionService;
using Code.Services.Windows;
using Code.Services.Windows.Factories;
using Code.StaticData.Windows;
using System;
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

        IWindow IInteractable.Interact(Action callback = null)
        {
            return _windowFactory.CreateWindow(transform, this, callback);
        }
    }
}
