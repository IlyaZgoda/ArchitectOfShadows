using Code.Gameplay.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.Windows.Factories
{
    public class InteractionWindowFactory : IWindowFactory<BaseInteractor>
    {
        public IWindow CreateWindow(Transform position, BaseInteractor interactor)
        {
            var prefab = Resources.Load<GameObject>("HUD/Windows/InteractionWindow");
            GameObject dialogueWindow = Object.Instantiate(prefab, position);
            var window = dialogueWindow.GetComponent<InteractionWindow>();
            window.SetInteractor(interactor);

            return window;
        }
    }
}
