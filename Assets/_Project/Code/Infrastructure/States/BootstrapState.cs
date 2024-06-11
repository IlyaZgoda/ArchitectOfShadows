using Code.Infrastructure.SceneManagement;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {         
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader) 
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public async UniTask Enter() => 
            await _sceneLoader.Load(Scenes.Initial, null, EnterLoadLevel);

        public UniTask Exit() =>
            default;

        public async void EnterLoadLevel() =>
            await _gameStateMachine.Enter<LoadMenuState>();
    }
}
