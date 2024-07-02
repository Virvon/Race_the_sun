using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }
}