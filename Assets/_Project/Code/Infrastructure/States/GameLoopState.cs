using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        [SerializeField] private IQuest testQuest;
        //private QuestSystem _questSystem;

        public GameLoopState() { 
            //_questSystem = questSystem;
        }

        public UniTask Enter() =>
            default;
        //{
            //_questSystem.RegisterAllQuests();
            //return UniTask.CompletedTask;
        //}
        
        public UniTask Exit() =>
            default;          
    }
}
