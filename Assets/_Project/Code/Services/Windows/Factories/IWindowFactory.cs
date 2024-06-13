using Code.Gameplay.Interaction;
using UnityEngine;

namespace Code.Services.Windows.Factories
{
    public interface IWindowFactory
    {
        public Window CreateWindow(Transform position, BaseInteractor interactor);
    }
}