using Code.Infrastructure.Factories;
using Code.Infrastructure.States;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private StateFactory _stateFactory;

        [Inject]
        public void Construct(StateFactory stateFactory, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
        }
            
        private async void Awake()
        {
            RegisterStates();
            await _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void RegisterStates()
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadMenuState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<MenuState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadLevelState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameLoopState>());
        }
    }   
}
