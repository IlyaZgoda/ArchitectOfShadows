using Code.Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;   

        public TState Create<TState>() where TState : class, IExitableState =>       
            _instantiator.Instantiate<TState>();   
    }
}
