using Code.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
    public interface IGameStateMachine 
    {
        UniTask Enter<TState>() where TState : class, IState;

        UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;

        void RegisterState<TState>(TState state) where TState : class, IExitableState;
    }
}
