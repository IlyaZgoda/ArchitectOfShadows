using Code.Infrastructure.SceneManagement;
using Code.StaticData;
using Code.StaticData.SceneManagement;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingProgressPresenter _loadingProgress;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingProgressPresenter loadingProgress, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingProgress = loadingProgress;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter()
        {
            await _sceneLoader.Load(Scenes.MainMenu, _loadingProgress, EnterLoadLevel);
            _loadingCurtain.Show();
        }    
                
        private async void EnterLoadLevel() =>       
            await _gameStateMachine.Enter<MenuState>();        

        public async UniTask Exit()
        {
            _loadingCurtain.Hide();
            await UniTask.Yield();
        }
            
    }
}
