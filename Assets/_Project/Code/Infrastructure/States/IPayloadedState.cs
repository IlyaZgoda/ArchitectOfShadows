using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}