using Code.Infrastructure.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Healing
{
    public class HealthPackSpawner : MonoBehaviour
    {
        private HealthFactory _healthFactory;

        [Inject]
        public void Construct(HealthFactory healthFactory) =>
            _healthFactory = healthFactory;

        async void Start()
        {
           await _healthFactory.CreateHealthPack(new Vector3(2.24000001f, -5.51999998f, 0));
        }
    }
}
