using Code.Services.Observable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.UI.HUD
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] HPBar _bar;
        private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus) =>
            _eventBus = eventBus;

        private void Start() =>
            _eventBus.OnPlayerHealthChange += UpdateHPBar;    

        private void UpdateHPBar(float value) =>
            _bar.SetValue(value);   

        private void OnDestroy() =>   
            _eventBus.OnPlayerHealthChange -= UpdateHPBar;
        
    }
}
