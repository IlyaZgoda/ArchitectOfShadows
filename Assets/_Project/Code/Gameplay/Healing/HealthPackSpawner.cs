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
        private EnemyFactory _enemyfactory;

        [Inject]
        public void Construct(HealthFactory healthFactory, EnemyFactory enemyFactory)
        {
            _healthFactory = healthFactory;
            _enemyfactory = enemyFactory;
        }
            

        async void Start()
        {
            //await _healthFactory.CreateHealthPack(new Vector3(2.24000001f, -5.51999998f, 0));
            _enemyfactory.CreateDigitalMag(new Vector3(2.24000001f, -5.51999998f, 0));
        }
    }
}
