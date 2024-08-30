using Assets.RaceTheSun.Sources.Gameplay.CollectItems.Items;
using Assets.RaceTheSun.Sources.Gameplay.Portals;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Sun;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory
{
    public interface IGameplayFactory
    {
        Spaceship Spaceship { get; }
        Gameplay.Spaceship.Plane Plane { get; }
        Sun Sun { get; }
        SpaceshipShieldPortal SpaceshipShieldPortal { get; }

        UniTask CreateHud();
        UniTask<Spaceship> CreateSpaceship();
        UniTask<GameObject> CreateTile(AssetReferenceGameObject tileReference, Vector3 position, Transform parent = null);
        UniTask CreateWorldGenerator();
        UniTask CreateSun();
        UniTask CreateShpaceshipShieldPortal();
        UniTask CreateGameOverPanel();
        UniTask<JumpBoost> CreateJumpBoost(Vector3 position, Transform parent = null);
        UniTask<Shield> CreateShield(Vector3 position, Transform parent = null);
        UniTask CreateShieldPortal(Vector3 postion);
        UniTask CreateBird();
        UniTask<ScoreItem> CreateScoreItem(Vector3 position);
        UniTask<SpeedBoost> CreateSpeedBoost(Vector3 position);
        UniTask CreateStageMusic();
        UniTask CreatePlane();
        UniTask CreateCollectItemsSoundEffects();
        UniTask CreatePortalSoundPlayer();
        UniTask CreateDestroySoundPlayer();
        UniTask CreateCollisionFx(Vector3 position, Transform parent);
    }
}