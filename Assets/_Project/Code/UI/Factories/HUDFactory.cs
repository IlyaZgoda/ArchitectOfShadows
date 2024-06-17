using Code.UI.HUD;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.UI.Factories
{
    public class HUDFactory
    {
        private IInstantiator _instantiator;

        public HUDFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;

        public async UniTask<ActorUI> CreateHUD()
        {
             var hud = _instantiator.InstantiatePrefabResourceForComponent<ActorUI>("HUD/hud");
             await UniTask.Yield();

            return hud;
        }
    }
}
