using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay
{
    public class GameplayBootstrapper : IInitializable
    {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly StatesFactory _statesFactory;

        public GameplayBootstrapper(IGameplayFactory gameplayFactory, GameplayStateMachine gameplayStateMachine, StatesFactory statesFactory)
        {
            _gameplayFactory = gameplayFactory;
            _gameplayStateMachine = gameplayStateMachine;
            _statesFactory = statesFactory;
        }

        public async void Initialize()
        {
            await _gameplayFactory.CreateStartCamera();
            await _gameplayFactory.CreateSpaceship();
            await _gameplayFactory.CreateShpaceshipShieldPortal();
            await _gameplayFactory.CreateWorldGenerator();
            await _gameplayFactory.CreateHud();
            await _gameplayFactory.CreateSpaceshipMainCamera();
            await _gameplayFactory.CreateSpaceshipSideCamera();
            await _gameplayFactory.CreateSpaceshipUpperCamera();
            await _gameplayFactory.CreateSun();
            await _gameplayFactory.CreateCollisionPortalCamera();
            await _gameplayFactory.CreateShieldCamera();
            await _gameplayFactory.CreateGameOverPanel();

            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameStartState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameLoopState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<RevivalState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<ResultState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameEndState>());

            await _gameplayStateMachine.Enter<GameStartState>();
        }
    }
}
