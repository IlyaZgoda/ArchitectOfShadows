using Code.Infrastructure.SceneManagement;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Code.Infrastructure.States
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public MenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public UniTask Enter() =>
            default;
            
        public void OnLoaded() {}

        public UniTask Exit() =>
            default;      
    }
}
