using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cinemachine;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameEndState : IState
    {
        private readonly Infrastructure.GameStateMachine.GameStateMachine _gameStateMachine;

        public GameEndState(Infrastructure.GameStateMachine.GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public UniTask Enter()
        {
            _gameStateMachine.Enter<MainMenuState>().Forget();
            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
