using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        [SerializeField] private IQuest testQuest;
        private QuestSystem _questSystem;

        public GameLoopState(QuestSystem questSystem) { 
            _questSystem = questSystem;
        }

        public UniTask Enter()
        {
            _questSystem.RegisterAllQuests();
            return UniTask.CompletedTask;
        }
        
        public UniTask Exit() =>
            default;          
    }
}
