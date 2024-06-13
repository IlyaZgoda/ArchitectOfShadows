using Code.Infrastructure.Factories;
using Code.Infrastructure.States;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class QuestInstaller : Installer<QuestInstaller>
    {
        public override void InstallBindings()
        {
            Container.
                BindInterfacesAndSelfTo<QuestSystem>().
                AsSingle().
                NonLazy();

            Container.
                BindInterfacesAndSelfTo<QuestFactory>().
                AsSingle().
                NonLazy();
        }
    }
}
