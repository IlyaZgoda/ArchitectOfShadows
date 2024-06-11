using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public UniTask Enter() =>
            default;
        
        public UniTask Exit() =>
            default;          
    }
}
