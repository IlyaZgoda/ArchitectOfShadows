using Code.Infrastructure.Factories;
using Code.Infrastructure.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            BindBootstrapperFactory();
            BindCoroutineRunner();
            BindSceneLoader();
            BindLoadingProgress();
            BindLoadingCurtain();
            BindGameStateMachine();           
        }

        private void BindBootstrapperFactory()
        {
            Container.
                Bind<BootstrapperFactory>().
                AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container.
                Bind<ICoroutineRunner>().
                To<CoroutineRunner>().
                FromComponentInNewPrefabResource(
                "Infrastructure/Bootstrapper").
                AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container.
                BindInterfacesAndSelfTo<LoadingCurtain>().
                FromComponentInNewPrefabResource(
                "HUD/LoadingCurtain").
                AsSingle();
        }

        private void BindGameStateMachine()
        {
            GameStateMachineInstaller.Install(Container);
        }

        private void BindSceneLoader()
        {
            Container.
                BindInterfacesAndSelfTo<SceneLoader>().
                AsSingle();
        }

        private void BindLoadingProgress()
        {
            Container.
                BindInterfacesAndSelfTo<LoadingProgressPresenter>().
                AsSingle();
        }

        private void BindQuestSystem()
        {
            QuestInstaller.Install(Container);
        }
    }
}

