using Code.Infrastructure.SceneManagement;
using Code.StaticData.SceneManagement;
using Code.UI.Factories;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Infrastructure.States
{
    public class CreditsState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public CreditsState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain)

        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter(string payload)
        {
            _loadingCurtain.Show();
            await _sceneLoader.Load(payload, null);
        }

        public async UniTask Exit()
        {
            _loadingCurtain.Hide();
            await UniTask.Yield();
        }
    }
}

