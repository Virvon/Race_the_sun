using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}