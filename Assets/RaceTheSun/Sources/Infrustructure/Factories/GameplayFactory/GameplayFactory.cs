using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Sun;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
        private readonly WorldGenerator.Factory _worldGeneratorFactory;
        private readonly VirtualCamera.Factory _virtualCameraFactory;
        private readonly Sun.Factory _sunFactory;

        public GameplayFactory(DiContainer container, HudFactory hudFactory, IStaticDataService staticDataService, Spaceship.Factory spaceshipFactory, Tile.Factory tileFactory, WorldGenerator.Factory worldGeneratorFactory, VirtualCamera.Factory virtualCameraFactory, Sun.Factory sunFactory)
        {
            _container = container;
            _hudFactory = hudFactory;
            _staticDataService = staticDataService;
            _spaceshipFactory = spaceshipFactory;
            _tileFactory = tileFactory;
            _worldGeneratorFactory = worldGeneratorFactory;
            _virtualCameraFactory = virtualCameraFactory;
            _sunFactory = sunFactory;
        }

        public async UniTask CreateSpaceship()
        {
            Spaceship spaceship = await _spaceshipFactory.Create(GameplayFactoryAssets.Spaceship);
            _container.Bind<Spaceship>().FromInstance(spaceship).AsSingle();
            //_container.Bind<SpaceshipDie>().FromInstance(spaceship.GetComponentInChildren<SpaceshipDie>()).AsSingle();
            //_container.Bind<SpaceshipJump>().FromInstance(spaceship.GetComponent<SpaceshipJump>()).AsSingle();
        }

        public async UniTask CreateHud() =>
            await _hudFactory.Create(GameplayFactoryAssets.Hud);

        public async UniTask CreatePlayerCharacterCamera() =>
            await _hudFactory.Create(GameplayFactoryAssets.PlayerCharacterCamera);

        public async UniTask<GameObject> CreateTile(AssetReferenceGameObject tileReference, Vector3 position, Transform parent)
        {
            Tile tile = await _tileFactory.Create(tileReference);

            tile.transform.parent = parent;
            tile.transform.position = position;

            return tile.gameObject;
        }

        public async UniTask CreateWorldGenerator()
        {
            WorldGenerator worldGenerator = await _worldGeneratorFactory.Create(GameplayFactoryAssets.WorldGenerator);
            _container.Bind<WorldGenerator>().FromInstance(worldGenerator).AsSingle();
        }

        public async UniTask CreateStartCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.StartCamera);
            _container.Bind<StartCamera>().FromInstance(virtualCamera.GetComponent<StartCamera>()).AsSingle();
        }

        public async UniTask CreateSpaceshipMainCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.SpaceshipMainCamera);
            _container.Bind<SpaceshipMainCamera>().FromInstance(virtualCamera.GetComponent<SpaceshipMainCamera>()).AsSingle();
        }

        public async UniTask CreateSpaceshipSideCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.SpaceshipSideCamera);
            _container.Bind<SpaceshipSideCamera>().FromInstance(virtualCamera.GetComponent<SpaceshipSideCamera>()).AsSingle(); ;
        }

        public async UniTask CreateSun()
        {
            Sun sun = await _sunFactory.Create(GameplayFactoryAssets.Sun);
            _container.Bind<Sun>().FromInstance(sun).AsSingle();
        }
    }
}
