using Cysharp.Threading.Tasks;
using Infrastructure.States;
using System;
using System.Collections.Generic;

namespace Code.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;      

        public GameStateMachine() =>       
            _states = new Dictionary<Type, IExitableState>();       

        public async UniTask Enter<TState>() where TState : class, IState
        {
            IState state = await ChangeState<TState>();
            await state.Enter();
        }

        public async UniTask Enter<TState, TPayLoad>(TPayLoad payload) where TState : class, IPayloadedState<TPayLoad>
        {
            TState state = await ChangeState<TState>();
            await state.Enter(payload);
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (_activeState != null) 
                await _activeState.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState: class, IExitableState =>
            _states[typeof(TState)] as TState;     

        public void RegisterState<TState>(TState state) where TState : class, IExitableState =>       
            _states.Add(typeof(TState), state);    
    }
}
