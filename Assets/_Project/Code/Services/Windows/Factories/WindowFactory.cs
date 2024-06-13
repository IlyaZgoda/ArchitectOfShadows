using Code.Gameplay.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.Windows.Factories
{
    public class WindowFactory : IWindowFactory
    {
        private Window _window;

        public WindowFactory()
        {
            Debug.Log("factory installed");
        }

        public Window CreateWindow(Transform position, BaseInteractor interactor)
        {
            var prefab = Resources.Load<GameObject>("HUD/Windows/InteractionWindow");
            GameObject dialogueWindow = Object.Instantiate(prefab, position);
            var window = dialogueWindow.GetComponent<Window>();
            window.SetInteractor(interactor);

            return window;
        }
    }
}
