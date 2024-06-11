using Code.Infrastructure.Factories;
using Code.Infrastructure.States;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.
                BindInterfacesAndSelfTo<GameStateMachine>().
                AsSingle().
                NonLazy();

            Container.
                BindInterfacesAndSelfTo<StateFactory>().
                AsSingle().
                NonLazy();           
        }
    }
}
