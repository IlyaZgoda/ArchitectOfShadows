using Code.Services.InteractionService;
using Code.Services.Windows;
using Code.Services.Windows.Factories;
using Code.StaticData.Windows;
using Code.StaticData.Windows.NPCDialogueConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Interaction
{
    public class NPCInteractor : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public DialogueConfig Data;
        private IWindowFactory<NPCInteractor> _windowFactory;

        [Inject]
        public void Construct(DialogueWindowFactory windowFactory) =>
            _windowFactory = windowFactory;

        public IWindow Interact(Action callback = null)
        {
            Debug.Log(callback.Target);
            return _windowFactory.CreateWindow(transform, this, callback);
        }
    }
}
