using Code.Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Code.Infrastructure.Factories
{
    public interface IStateFactory
    {
        public TState Create<TState>() where TState : class, IExitableState;
    }
}
