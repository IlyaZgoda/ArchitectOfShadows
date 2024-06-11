using Code.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {      
        private BootstrapperFactory _bootstrapperFactory = null;

        [Inject]
        public void Construct(BootstrapperFactory bootstrapperFactory) =>       
            _bootstrapperFactory = bootstrapperFactory;        

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<Bootstrapper>();

            if (bootstrapper != null) return;

            _bootstrapperFactory.Create();
        }
    }
}

