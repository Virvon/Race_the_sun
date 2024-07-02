using Cysharp.Threading.Tasks;
using System;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}