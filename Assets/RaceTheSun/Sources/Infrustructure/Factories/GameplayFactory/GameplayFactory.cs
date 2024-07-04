using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
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
        private readonly Tile.Factory _tileFactory;

        public GameplayFactory(DiContainer container, HudFactory hudFactory, IStaticDataService staticDataService, Spaceship.Factory spaceshipFactory, Tile.Factory tileFactory)
        {
            _container = container;
            _hudFactory = hudFactory;
            _staticDataService = staticDataService;
            _spaceshipFactory = spaceshipFactory;
            _tileFactory = tileFactory;
        }

        public async UniTask CreateSpaceship()
        {
            Spaceship spaceship = await _spaceshipFactory.Create(GameplayFactoryAssets.Spaceship);
            _container.Bind<Spaceship>().FromInstance(spaceship).AsSingle();
            _container.Bind<SpaceshipDie>().FromInstance(spaceship.GetComponentInChildren<SpaceshipDie>()).AsSingle();
            _container.Bind<SpaceshipJump>().FromInstance(spaceship.GetComponent<SpaceshipJump>()).AsSingle();
        }

        public async UniTask CreateHud() =>
            await _hudFactory.Create(GameplayFactoryAssets.Hud);

        public async UniTask CreatePlayerCharacterCamera() =>
            await _hudFactory.Create(GameplayFactoryAssets.PlayerCharacterCamera);

        public async UniTask<GameObject> CreateTile(Vector3 position, Transform parent)
        {
            Tile tile = await _tileFactory.Create(GameplayFactoryAssets.Tile);

            tile.transform.parent = parent;
            tile.transform.position = position;

            return tile.gameObject;
        }

        public async UniTask CreateWorldGenerator() =>
            await _hudFactory.Create(GameplayFactoryAssets.WorldGenerator);
    }
}
