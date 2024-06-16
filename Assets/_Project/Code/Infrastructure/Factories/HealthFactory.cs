using Code.Gameplay.Healing;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public class HealthFactory
    {
        public async UniTask<HealPack> CreateHealthPack(Vector3 at)
        {
            var prefab = await Resources.LoadAsync("Healing/HealthPack");
            GameObject go = (GameObject)Object.Instantiate(prefab, at, Quaternion.identity);
            HealPack healPack = go.GetComponent<HealPack>();    

            return healPack;
        }
    }
}
