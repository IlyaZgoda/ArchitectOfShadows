using Code.Services.Observable;
using Code.Services.Windows.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            BindChoiceWindowFactory();
            BindInteractionWindowFactory();
            BindDialogueWindowFactory();

            BindEventBus();
        }

        private void BindInteractionWindowFactory()
        {
            Container.
                BindInterfacesAndSelfTo<InteractionWindowFactory>().
                AsSingle();
        }

        private void BindDialogueWindowFactory()
        {
            Container.
                BindInterfacesAndSelfTo<DialogueWindowFactory>().
                AsSingle();
        }
        private void BindChoiceWindowFactory()
        {
            Container.
                BindInterfacesAndSelfTo<ChoiceWindowFactory>().
                AsSingle();
        }
        private void BindEventBus()
        {
            Container.
                BindInterfacesAndSelfTo<EventBus>().
                AsSingle();
        }
    }
}
