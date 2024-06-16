using Code.Services.Windows;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class EnemyFactory
    {
        private IInstantiator _instantiator;

        public EnemyFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;

        public void CreateDigitalMag(Vector3 at) =>
            _instantiator.InstantiatePrefabResourceForComponent<DigitalMag>("Entities/Enemy/Digital Mag/Digital Mag", at, Quaternion.identity, null);

        public void CreateElectroAnomalies(Vector3 at) =>
            _instantiator.InstantiatePrefabResourceForComponent<ElectroAnomalies>("Entities/Enemy/Electro Anomalies/Electro Anomalies", at, Quaternion.identity, null);

        public void CreateProcessorBoogies(Vector3 at) =>
            _instantiator.InstantiatePrefabResourceForComponent<ProcessorBoogies>("Entities/Enemy/Processor Boogies/Processor Boogies", at, Quaternion.identity, null);

        public void CreateViralPredatorOne(Vector3 at) =>
           _instantiator.InstantiatePrefabResourceForComponent<AttackViralPredator>("Entities/Enemy/Viral Predator/Viral Predator", at, Quaternion.identity, null);
        public void CreateViralPredatorMany(Vector3 at, int count)
        {
            for (int i = 0; i < count; i++)
                _instantiator.InstantiatePrefabResourceForComponent<AttackViralPredator>("Entities/Enemy/Viral Predator/Viral Predator", at, Quaternion.identity, null);
        }
           

    }
}
