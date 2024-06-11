using Code.Infrastructure.SceneManagement;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingProgressPresenter _loadingProgress;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingProgressPresenter loadingProgress)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingProgress = loadingProgress;
        }
        public async UniTask Enter(string payload)
        {
            await _sceneLoader.Load(payload, _loadingProgress, EnterLoadLevel);         
        }

        private async void EnterLoadLevel() =>        
            await _gameStateMachine.Enter<GameLoopState>();

        public UniTask Exit() =>
            default;    
    }
}
