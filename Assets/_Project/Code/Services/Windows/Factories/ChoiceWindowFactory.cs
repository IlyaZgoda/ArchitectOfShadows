using Code.Gameplay.Interaction;
using Code.Services.Windows;
using Code.Services.Windows.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Windows.Factories
{
    public class ChoiceWindowFactory : IWindowFactory<NPCInteractor>
    {
        public IWindow CreateWindow(Transform position, NPCInteractor interactor)
        {
            var prefab = Resources.Load<GameObject>("HUD/Windows/ChoiceWindow");
            GameObject dialogueWindow = UnityEngine.Object.Instantiate(prefab, position);
            var window = dialogueWindow.GetComponent<ChoiceWindow>();
            window.SetInteractor(interactor);

            return window;
        }
    }
}
