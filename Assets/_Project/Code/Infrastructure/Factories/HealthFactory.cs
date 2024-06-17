using Code.Gameplay.Healing;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public class HealthFactory
    {
        public async UniTask CreateHealthPack(Vector3 at)
        {
            var prefab = await Resources.LoadAsync("Healing/HealthPack");
            Object.Instantiate(prefab, at, Quaternion.identity);

        }
    }
}
