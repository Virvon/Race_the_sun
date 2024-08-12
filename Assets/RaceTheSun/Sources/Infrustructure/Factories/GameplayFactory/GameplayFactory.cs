﻿using Assets.RaceTheSun.Sources.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.Bird;
using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Gameplay.CollectItems;
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
        private readonly GameplayCameras _cameras;
        private readonly SpaceshipShieldPortal.Factory _spaceshipShieldPortalFactory;
        private readonly GameOverPanel.Factory _gameOverPanelFactory;
        private readonly JumpBoost.Factory _jumpBoostFactory;
        private readonly Shield.Factory _shieldFactory;
        private readonly ShieldPortal.Factory _shieldPortalFactory;
        private readonly Bird.Factory _birdFactory;
        private readonly ScoreItem.Factory _scoreItemFactory;
        private readonly SpeedBoost.Factory _speedBoostFactory;

        private SpaceshipDie _spaceshipDie;

        public GameplayFactory(DiContainer container, Hud.Factory hudFactory, IStaticDataService staticDataService, Spaceship.Factory spaceshipFactory, Tile.Factory tileFactory, WorldGenerator.Factory worldGeneratorFactory, VirtualCamera.Factory virtualCameraFactory, Sun.Factory sunFactory, DistanceObservable distanceObservable, GameplayCameras cameras, SpaceshipShieldPortal.Factory spaceshipShieldPortalFactory, GameOverPanel.Factory gameOverPanelFactory, JumpBoost.Factory jumpBoostFactory, Shield.Factory shieldFactory, ShieldPortal.Factory shieldPortalFactory, Bird.Factory birdFactory, ScoreItem.Factory scoreItemFactory, SpeedBoost.Factory speedBoostFactory)
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
            _jumpBoostFactory = jumpBoostFactory;
            _shieldFactory = shieldFactory;
            _shieldPortalFactory = shieldPortalFactory;
            _birdFactory = birdFactory;
            _scoreItemFactory = scoreItemFactory;
            _speedBoostFactory = speedBoostFactory;
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
            _spaceshipDie.Init(spaceshipShieldPortal);
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

        public async UniTask<Spaceship> CreateSpaceship()
        {
            Spaceship spaceship = await _spaceshipFactory.Create(GameplayFactoryAssets.Spaceship);
            _container.Bind<Spaceship>().FromInstance(spaceship).AsSingle();
            _container.Bind<SpaceshipMovement>().FromInstance(spaceship.GetComponentInChildren<SpaceshipMovement>()).AsSingle();
            _container.Bind<StartMovement>().FromInstance(spaceship.GetComponentInChildren<StartMovement>()).AsSingle();
            _container.Bind<CollisionPortalPoint>().FromInstance(spaceship.GetComponentInChildren<CollisionPortalPoint>()).AsSingle();
            _distanceObservable.Init(spaceship);
            _container.Bind<Battery>().FromInstance(spaceship.GetComponentInChildren<Battery>()).AsSingle();
            

            _spaceshipDie = spaceship.GetComponentInChildren<SpaceshipDie>();

            return spaceship;
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

        public async UniTask<JumpBoost> CreateJumpBoost(Vector3 position, Transform parent = null)
        {
            JumpBoost jumpBoost = await _jumpBoostFactory.Create(GameplayFactoryAssets.JumpBoost);

            jumpBoost.transform.position = position;
            jumpBoost.transform.parent = parent;

            return jumpBoost;
        }

        public async UniTask<Shield> CreateShield(Vector3 position, Transform parent = null)
        {
            Shield shield = await _shieldFactory.Create(GameplayFactoryAssets.Shield);

            shield.transform.position = position;
            shield.transform.parent = parent;

            return shield;
        }

        public async UniTask CreateShieldPortal(Vector3 position)
        {
            ShieldPortal shieldPortal = await _shieldPortalFactory.Create(GameplayFactoryAssets.ShieldPortal);

            shieldPortal.transform.position = position;
        }

        public async UniTask CreateBird()
        {
            Bird bird = await _birdFactory.Create(GameplayFactoryAssets.Bird);

            _container.Bind<Bird>().FromInstance(bird).AsSingle();
        }

        public async UniTask<ScoreItem> CreateScoreItem(Vector3 position)
        {
            ScoreItem scoreItem = await _scoreItemFactory.Create(GameplayFactoryAssets.ScoreItem);

            scoreItem.transform.position = position;
            
            return scoreItem;
        }

        public async UniTask<SpeedBoost> CreateSpeedBoost(Vector3 position)
        {
            SpeedBoost speedBoost = await _speedBoostFactory.Create(GameplayFactoryAssets.SpeedBoost);

            speedBoost.transform.position = position;

            return speedBoost;
        }
    }
}
