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
            BindWindowFactory();
            BindEventBus();
        }

        private void BindWindowFactory()
        {
            Container.
                BindInterfacesAndSelfTo<WindowFactory>().
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
