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
    public class QuestFactory
    {
        private readonly IInstantiator _instantiator;

        public QuestFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;

        public TQuest Create<TQuest>() where TQuest : class, IQuest =>
            _instantiator.Instantiate<TQuest>();
    }
}
