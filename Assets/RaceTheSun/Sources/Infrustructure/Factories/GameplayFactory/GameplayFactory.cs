using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Sun;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI.GameOverPanel;
using Assets.RaceTheSun.Sources.UI.ScoreView;
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
        private readonly Hud.Factory _hudFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly Spaceship.Factory _spaceshipFactory;
        private readonly Tile.Factory _tileFactory;
        private readonly WorldGenerator.Factory _worldGeneratorFactory;
        private readonly VirtualCamera.Factory _virtualCameraFactory;
        private readonly Sun.Factory _sunFactory;
        private readonly DistanceObservable _distanceObservable;
        private readonly Cameras _cameras;
        private readonly SpaceshipShieldPortal.Factory _spaceshipShieldPortalFactory;
        private readonly GameOverPanel.Factory _gameOverPanelFactory;

        public GameplayFactory(DiContainer container, Hud.Factory hudFactory, IStaticDataService staticDataService, Spaceship.Factory spaceshipFactory, Tile.Factory tileFactory, WorldGenerator.Factory worldGeneratorFactory, VirtualCamera.Factory virtualCameraFactory, Sun.Factory sunFactory, DistanceObservable distanceObservable, Cameras cameras, SpaceshipShieldPortal.Factory spaceshipShieldPortalFactory, GameOverPanel.Factory gameOverPanelFactory)
        {
            _container = container;
            _hudFactory = hudFactory;
            _staticDataService = staticDataService;
            _spaceshipFactory = spaceshipFactory;
            _tileFactory = tileFactory;
            _worldGeneratorFactory = worldGeneratorFactory;
            _virtualCameraFactory = virtualCameraFactory;
            _sunFactory = sunFactory;
            _distanceObservable = distanceObservable;
            _cameras = cameras;
            _spaceshipShieldPortalFactory = spaceshipShieldPortalFactory;
            _gameOverPanelFactory = gameOverPanelFactory;
        }

        public async UniTask CreateGameOverPanel()
        {
            GameOverPanel gameOverPanel = await _gameOverPanelFactory.Create(GameplayFactoryAssets.GameOverPanel);
            _container.Bind<RevivalPanel>().FromInstance(gameOverPanel.GetComponentInChildren<RevivalPanel>()).AsSingle();
            _container.Bind<ResultPanel>().FromInstance(gameOverPanel.GetComponentInChildren<ResultPanel>()).AsSingle();
        }

        public async UniTask CreateShpaceshipShieldPortal()
        {
            SpaceshipShieldPortal spaceshipShieldPortal = await _spaceshipShieldPortalFactory.Create(GameplayFactoryAssets.SpaceshipShieldPortal);
            _container.Bind<SpaceshipShieldPortal>().FromInstance(spaceshipShieldPortal).AsSingle();
        }

        public async UniTask CreateShieldCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.ShieldPortalCamera);
            _cameras.Init(virtualCamera.GetComponent<ShieldPortalCamera>());
        }

        public async UniTask CreateCollisionPortalCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.CollisionPortalCamera);
            _cameras.Init(virtualCamera.GetComponent<CollisionPortalCamera>());
        }

        public async UniTask CreateSpaceshipUpperCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.SpaceshipUpperCamera);
            _cameras.Init(virtualCamera.GetComponent<SpaceshipUpperCamera>());
        }

        public async UniTask CreateSpaceship()
        {
            Spaceship spaceship = await _spaceshipFactory.Create(GameplayFactoryAssets.Spaceship);
            _container.Bind<Spaceship>().FromInstance(spaceship).AsSingle();
            _container.Bind<SpaceshipMovement>().FromInstance(spaceship.GetComponentInChildren<SpaceshipMovement>()).AsSingle();
            _container.Bind<StartMovement>().FromInstance(spaceship.GetComponentInChildren<StartMovement>()).AsSingle();

            _distanceObservable.Init(spaceship);
        }

        public async UniTask CreateHud()
        {
            Hud hud = await _hudFactory.Create(GameplayFactoryAssets.Hud);
        }

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
            _cameras.Init(virtualCamera.GetComponent<StartCamera>());
        }

        public async UniTask CreateSpaceshipMainCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.SpaceshipMainCamera);
            _container.Bind<SpaceshipMainCamera>().FromInstance(virtualCamera.GetComponent<SpaceshipMainCamera>()).AsSingle();
            _cameras.Init(virtualCamera.GetComponent<SpaceshipMainCamera>());
        }

        public async UniTask CreateSpaceshipSideCamera()
        {
            VirtualCamera virtualCamera = await _virtualCameraFactory.Create(GameplayFactoryAssets.SpaceshipSideCamera);
            _container.Bind<SpaceshipSideCamera>().FromInstance(virtualCamera.GetComponent<SpaceshipSideCamera>()).AsSingle(); ;
            _cameras.Init(virtualCamera.GetComponent<SpaceshipSideCamera>());
        }

        public async UniTask CreateSun()
        {
            Sun sun = await _sunFactory.Create(GameplayFactoryAssets.Sun);
            _container.Bind<Sun>().FromInstance(sun).AsSingle();
        }
    }
}
