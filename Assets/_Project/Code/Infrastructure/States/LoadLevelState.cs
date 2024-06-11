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
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingProgressPresenter loadingProgress, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingProgress = loadingProgress;
            _loadingCurtain = loadingCurtain;
        }
        public async UniTask Enter(string payload)
        {
            _loadingCurtain.Show();
            await _sceneLoader.Load(payload, _loadingProgress, EnterLoadLevel);                
        }

        private async void EnterLoadLevel() =>        
            await _gameStateMachine.Enter<GameLoopState>();

        public async UniTask Exit()
        {
            _loadingCurtain.Hide();
            await UniTask.Yield();
        }
           
    }
}
