using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay
{
    public class GameplayBootstrapper : IInitializable
    {
        private readonly IGameplayFactory _gameplayFactory;

        public GameplayBootstrapper(IGameplayFactory gameplayFactory)
        {
            _gameplayFactory = gameplayFactory;
        }

        public async void Initialize()
        {
            await _gameplayFactory.CreateSpaceship();
            await _gameplayFactory.CreateWorldGenerator();
        }
    }
}
