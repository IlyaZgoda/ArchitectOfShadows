using Code.Gameplay.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Services.Windows.Factories
{
    public class DialogueWindowFactory : IWindowFactory<NPCInteractor>
    {
        private IInstantiator _instantiator;

        public DialogueWindowFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;

        public IWindow CreateWindow(Transform position, NPCInteractor interactor, Action callback = null)
        {
            var window = _instantiator.InstantiatePrefabResourceForComponent<DialogueWindow>("HUD/Windows/DialogueWindow");

            window.SetInteractor(interactor);
            window.Set(position);
            window.SetCallback(callback);

            return window;
        }
    }
}
