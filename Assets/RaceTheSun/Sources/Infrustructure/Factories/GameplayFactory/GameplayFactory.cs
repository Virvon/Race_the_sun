using Assets.MyBakery.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly DiContainer _container;
        private readonly HudFactory _hudFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly Spaceship.Factory _spaceshipFactory;

        public GameplayFactory(DiContainer container, HudFactory hudFactory, IStaticDataService staticDataService, Spaceship.Factory spaceshipFactory)
        {
            _container = container;
            _hudFactory = hudFactory;
            _staticDataService = staticDataService;
            _spaceshipFactory = spaceshipFactory;
        }

        public async UniTask CreateSpaceship()
        {
            await _spaceshipFactory.Create(GameplayFactoryAssets.Spaceship);
        }

        public async UniTask CreateHud() =>
            await _hudFactory.Create(GameplayFactoryAssets.Hud);

        public async UniTask CreatePlayerCharacter(Vector3 position)
        {
            //PlayerCharacter playerCharacter = await _playerCharacterFactory.Create(GameplayFactoryAssets.PlayerCharacter);
            //playerCharacter.transform.position = position;
            //_container.Bind<PlayerCharacter>().FromInstance(playerCharacter).AsSingle();

            //PlayerCharacterMovement x = playerCharacter.GetComponent<PlayerCharacterMovement>();
            //_container.Bind<PlayerCharacterMovement>().FromInstance(x).AsSingle();
        }

        public async UniTask CreatePlayerCharacterCamera() =>
            await _hudFactory.Create(GameplayFactoryAssets.PlayerCharacterCamera);
    }
}
