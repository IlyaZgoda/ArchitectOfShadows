using Code.Infrastructure.SceneManagement;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Code.UI.Factories;
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
        private readonly HUDFactory _HUDFactory;

        public LoadLevelState(
            GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            LoadingProgressPresenter loadingProgress, 
            LoadingCurtain loadingCurtain, 
            HUDFactory HUDFactory)

        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingProgress = loadingProgress;
            _loadingCurtain = loadingCurtain;
            _HUDFactory = HUDFactory;
        }

        public async UniTask Enter(string payload)
        {
            _loadingCurtain.Show();
            await _sceneLoader.Load(payload, _loadingProgress, EnterLoadLevel);                
        }

        private async void EnterLoadLevel()
        {
            await _gameStateMachine.Enter<GameLoopState>();
            await InitHUD();
        }       

        public async UniTask Exit()
        {
            _loadingCurtain.Hide();
            await UniTask.Yield();
        }

        private async UniTask InitHUD() =>  
            await _HUDFactory.CreateHUD();  
    }
}
