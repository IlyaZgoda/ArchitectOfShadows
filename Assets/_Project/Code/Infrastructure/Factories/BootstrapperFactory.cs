using Zenject;

namespace Code.Infrastructure.Factories
{
    public class BootstrapperFactory
    {
        private IInstantiator _instantiator;

        public BootstrapperFactory(IInstantiator instantiator) =>        
            _instantiator = instantiator;

        public Bootstrapper Create() =>
            _instantiator.InstantiatePrefabResourceForComponent<Bootstrapper>(
                "Infrastructure/Bootstrapper");
    }
}
