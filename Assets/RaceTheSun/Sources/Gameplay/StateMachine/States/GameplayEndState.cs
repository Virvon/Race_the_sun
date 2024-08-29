using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameplayEndState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameplayEndState(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public UniTask Enter()
        {
            _gameStateMachine.Enter<MainMenuState>().Forget();
            return default;
        }

        public UniTask Exit() =>
            default;
    }
}
