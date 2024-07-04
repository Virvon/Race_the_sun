using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
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
            await _gameplayFactory.CreateSpaceship();
            await _gameplayFactory.CreateWorldGenerator();
            await _gameplayFactory.CreateHud();

            //_gameplayStateMachine.RegisterState()
        }
    }
}
