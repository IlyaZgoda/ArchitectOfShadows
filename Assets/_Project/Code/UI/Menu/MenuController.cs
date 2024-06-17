using Code.Infrastructure;
using Code.Infrastructure.States;
using Code.StaticData.SceneManagement;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Code.UI.Menu
{
    public class MenuController : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        [SerializeField] private MenuButton[] _menuButtons;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine) =>      
            _gameStateMachine = gameStateMachine;
        
        private void Start()
        {
            foreach (var menuButton in _menuButtons)          
                menuButton.ClickEvent += OnMenuButtonClick;
        }

        private async void OnMenuButtonClick(string levelKey)
        {
            if (levelKey == Scenes.Credits)
                await _gameStateMachine.Enter<CreditsState, string>(levelKey);
            else
                await _gameStateMachine.Enter<LoadLevelState, string>(levelKey);
        }
            
    }  
}


