using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;
using Assets.RaceTheSun.Sources.Infrustructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine.GameStateMachine _gameStateMachine;
        private StatesFactory _statesFactory;

        [Inject]
        private void Construct(GameStateMachine.GameStateMachine gameStateMachine, StatesFactory statesFactory)
        {
            _gameStateMachine = gameStateMachine;
            _statesFactory = statesFactory;
        }

        private void Start()
        {
            _gameStateMachine.RegisterState(_statesFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<LoadProgressState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<MainMenuState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<GameLoopState>());

#pragma warning disable CS4014
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}