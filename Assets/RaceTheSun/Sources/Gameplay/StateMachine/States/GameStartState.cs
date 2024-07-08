using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameStartState : IState
    {
        private readonly GameplayFactory _gameplayFactory;

        public GameStartState(GameplayFactory gameplayFactory)
        {
            _gameplayFactory = gameplayFactory;
        }

        public async UniTask Enter()
        {
            await _gameplayFactory.CreateStartCamera();
        }

        public UniTask Exit()
        {
            throw new NotImplementedException();
        }
    }
}
