using Code.Gameplay.Interaction;
using Code.Services.InteractionService;
using UnityEngine;

namespace Code.Services.Windows.Factories
{
    public interface IWindowFactory<TInteractor> where TInteractor : class, IInteractable
    {
        public IWindow CreateWindow(Transform position, TInteractor interactor);
    }
}