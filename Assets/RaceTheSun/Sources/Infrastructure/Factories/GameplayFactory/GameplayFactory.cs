using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.Gameplay.Bird;
using Assets.RaceTheSun.Sources.Gameplay.CollectItems.Items;
using Assets.RaceTheSun.Sources.Gameplay.Portals;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Battery;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Collision;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement;
using Assets.RaceTheSun.Sources.Gameplay.Sun;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.Tiles;
using Assets.RaceTheSun.Sources.UI.GameOverPanel;
using Assets.RaceTheSun.Sources.UI.Hud;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly DiContainer _container;
        private readonly Hud.Factory _hudFactory;
        private readonly Spaceship.Factory _spaceshipFactory;
        private readonly Tile.Factory _tileFactory;
        private readonly WorldGenerator.Factory _worldGeneratorFactory;
        private readonly Sun.Factory _sunFactory;
        private readonly SpaceshipShieldPortal.Factory _spaceshipShieldPortalFactory;
        private readonly GameOverPanel.Factory _gameOverPanelFactory;
        private readonly JumpBoost.Factory _jumpBoostFactory;
        private readonly Shield.Factory _shieldFactory;
        private readonly ShieldPortal.Factory _shieldPortalFactory;
        private readonly Bird.Factory _birdFactory;
        private readonly ScoreItem.Factory _scoreItemFactory;
        private readonly SpeedBoost.Factory _speedBoostFactory;
        private readonly StageMusic.Factory _stageMusicFactory;
        private readonly Gameplay.Spaceship.Plane.Factory _planeFactory;
        private readonly CollectItemsSoundEffects.Factory _collectItemsSoundEffectsFactory;
        private readonly CollisionFx.Factory _collisonFxFactory;
        private readonly SoundPlayer.Factory _soundPlayerFactory;

        public GameplayFactory(
            DiContainer container,
            Hud.Factory hudFactory,
            Spaceship.Factory spaceshipFactory,
            Tile.Factory tileFactory,
            WorldGenerator.Factory worldGeneratorFactory,
            Sun.Factory sunFactory,
            SpaceshipShieldPortal.Factory spaceshipShieldPortalFactory,
            GameOverPanel.Factory gameOverPanelFactory,
            JumpBoost.Factory jumpBoostFactory,
            Shield.Factory shieldFactory,
            ShieldPortal.Factory shieldPortalFactory,
            Bird.Factory birdFactory,
            ScoreItem.Factory scoreItemFactory,
            SpeedBoost.Factory speedBoostFactory,
            StageMusic.Factory stageMusicFactory,
            Gameplay.Spaceship.Plane.Factory planeFactory,
            CollectItemsSoundEffects.Factory collectItemsSoundEffectsFactory,
            CollisionFx.Factory collisonFxFactory,
            SoundPlayer.Factory soundPlayerFactory)
        {
            _container = container;
            _hudFactory = hudFactory;
            _spaceshipFactory = spaceshipFactory;
            _tileFactory = tileFactory;
            _worldGeneratorFactory = worldGeneratorFactory;
            _sunFactory = sunFactory;
            _spaceshipShieldPortalFactory = spaceshipShieldPortalFactory;
            _gameOverPanelFactory = gameOverPanelFactory;
            _jumpBoostFactory = jumpBoostFactory;
            _shieldFactory = shieldFactory;
            _shieldPortalFactory = shieldPortalFactory;
            _birdFactory = birdFactory;
            _scoreItemFactory = scoreItemFactory;
            _speedBoostFactory = speedBoostFactory;
            _stageMusicFactory = stageMusicFactory;
            _planeFactory = planeFactory;
            _collectItemsSoundEffectsFactory = collectItemsSoundEffectsFactory;
            _collisonFxFactory = collisonFxFactory;
            _soundPlayerFactory = soundPlayerFactory;
        }

        public Spaceship Spaceship { get; private set; }
        public SpaceshipShieldPortal SpaceshipShieldPortal { get; private set; }
        public Sun Sun { get; private set; }
        public Gameplay.Spaceship.Plane Plane { get; private set; }

        public async UniTask CreateGameOverPanel()
        {
            GameOverPanel gameOverPanel = await _gameOverPanelFactory.Create(GameplayFactoryAssets.GameOverPanel);
            _container.Bind<RevivalPanel>().FromInstance(gameOverPanel.GetComponentInChildren<RevivalPanel>()).AsSingle();
            _container.Bind<ResultPanel>().FromInstance(gameOverPanel.GetComponentInChildren<ResultPanel>()).AsSingle();
        }

        public async UniTask CreateShpaceshipShieldPortal()
        {
            SpaceshipShieldPortal = await _spaceshipShieldPortalFactory.Create(GameplayFactoryAssets.SpaceshipShieldPortal);
            _container.Bind<SpaceshipShieldPortal>().FromInstance(SpaceshipShieldPortal).AsSingle();
        }

        public async UniTask<Spaceship> CreateSpaceship()
        {
            Spaceship = await _spaceshipFactory.Create(GameplayFactoryAssets.Spaceship);

            _container.Bind<Spaceship>().FromInstance(Spaceship).AsSingle();
            _container.Bind<SpaceshipMovement>().FromInstance(Spaceship.GetComponentInChildren<SpaceshipMovement>()).AsSingle();
            _container.Bind<CutSceneMovement>().FromInstance(Spaceship.GetComponentInChildren<CutSceneMovement>()).AsSingle();
            _container.Bind<CollisionPortalPoint>().FromInstance(Spaceship.GetComponentInChildren<CollisionPortalPoint>()).AsSingle();
            _container.Bind<Battery>().FromInstance(Spaceship.GetComponentInChildren<Battery>()).AsSingle();
            _container.Bind<SpaceshipTurning>().FromInstance(Spaceship.GetComponentInChildren<SpaceshipTurning>()).AsSingle();
            _container.Bind<SpaceshipDie>().FromInstance(Spaceship.GetComponentInChildren<SpaceshipDie>()).AsSingle();
            _container.Bind<SpaceshipJump>().FromInstance(Spaceship.GetComponentInChildren<SpaceshipJump>()).AsSingle();

            return Spaceship;
        }

        public async UniTask CreateHud()
        {
            Hud hud = await _hudFactory.Create(GameplayFactoryAssets.Hud);

            PerfectStagePanel perfectStagePanel = hud.GetComponentInChildren<PerfectStagePanel>();
            perfectStagePanel.gameObject.SetActive(false);

            _container.Bind<PerfectStagePanel>().FromInstance(perfectStagePanel).AsSingle();
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

        public async UniTask CreateSun()
        {
            Sun = await _sunFactory.Create(GameplayFactoryAssets.Sun);

            _container.Bind<Sun>().FromInstance(Sun).AsSingle();
            _container.Bind<SkyboxSettingsChanger>().FromInstance(Sun.GetComponent<SkyboxSettingsChanger>()).AsSingle();
        }

        public async UniTask<JumpBoost> CreateJumpBoost(Vector3 position, Transform parent = null)
        {
            JumpBoost jumpBoost = await _jumpBoostFactory.Create(GameplayFactoryAssets.JumpBoost);

            jumpBoost.transform.parent = parent;
            jumpBoost.transform.position = position;

            return jumpBoost;
        }

        public async UniTask<Shield> CreateShield(Vector3 position, Transform parent = null)
        {
            Shield shield = await _shieldFactory.Create(GameplayFactoryAssets.Shield);

            shield.transform.parent = parent;
            shield.transform.position = position;

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

        public async UniTask CreateStageMusic()
        {
            StageMusic stageMusic = await _stageMusicFactory.Create(GameplayFactoryAssets.StageMusic);

            _container.Bind<StageMusic>().FromInstance(stageMusic).AsSingle();
        }

        public async UniTask CreatePlane()
        {
            Plane = await _planeFactory.Create(GameplayFactoryAssets.Plane);

            _container.Bind<Gameplay.Spaceship.Plane>().FromInstance(Plane).AsSingle();
        }

        public async UniTask CreateCollectItemsSoundEffects()
        {
            CollectItemsSoundEffects collectItemsSoundEffects = await _collectItemsSoundEffectsFactory.Create(GameplayFactoryAssets.CollectItemsSoundEffects);

            _container.Bind<CollectItemsSoundEffects>().FromInstance(collectItemsSoundEffects).AsSingle();
        }

        public async UniTask CreatePortalSoundPlayer() =>
            await CreateSoundPlayer(GameplayFactoryInjectId.PortalSound, GameplayFactoryAssets.PortalSoundPlayer);

        public async UniTask CreateDestroySoundPlayer() =>
            await CreateSoundPlayer(GameplayFactoryInjectId.DestroySound, GameplayFactoryAssets.DestroySoundPlayer);

        public async UniTask CreateCollisionFx(Vector3 position, Transform parent)
        {
            CollisionFx collisionFx = await _collisonFxFactory.Create(GameplayFactoryAssets.CollisionFx);
            collisionFx.transform.position = position;
            collisionFx.transform.parent = parent;
        }

        private async UniTask CreateSoundPlayer(string injectId, string path)
        {
            SoundPlayer soundPlayer = await _soundPlayerFactory.Create(path);

            _container.Bind<SoundPlayer>().WithId(injectId).FromInstance(soundPlayer);
        }
    }
}
