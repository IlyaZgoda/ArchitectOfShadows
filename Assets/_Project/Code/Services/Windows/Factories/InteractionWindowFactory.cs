using Code.Gameplay.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.Windows.Factories
{
    public class InteractionWindowFactory : IWindowFactory<BaseInteractor>
    {
        public IWindow CreateWindow(Transform position, BaseInteractor interactor, Action callback = null)
        {
            var prefab = Resources.Load<GameObject>("HUD/Windows/InteractionWindow");
            GameObject dialogueWindow = UnityEngine.Object.Instantiate(prefab, position);
            var window = dialogueWindow.GetComponent<InteractionWindow>();
            window.SetInteractor(interactor);
            window.SetCallback(callback);

            return window;
        }
    }
}
