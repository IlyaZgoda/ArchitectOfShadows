using Code.StaticData;
using Code.StaticData.SceneManagement;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private LevelStaticData _levelStaticData;
        private Button _button;
        public LevelStaticData LevelStaticData => _levelStaticData;

        public event Action<string> ClickEvent; 

        private void Awake()
        {
            if(TryGetComponent(out _button))           
                _button.onClick.AddListener(()
                    => ClickEvent?.Invoke(LevelStaticData.LevelKey));           
        }
    }
}
