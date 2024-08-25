using Assets.RaceTheSun.Sources.Gameplay.CollectItems;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory
{
    public interface IGameplayFactory
    {
        UniTask CreateHud();
        UniTask<Spaceship> CreateSpaceship();
        UniTask<GameObject> CreateTile(AssetReferenceGameObject tileReference, Vector3 position, Transform parent = null);
        UniTask CreateWorldGenerator();
        UniTask CreateStartCamera();
        UniTask CreateSpaceshipMainCamera();
        UniTask CreateSpaceshipSideCamera();
        UniTask CreateSun();
        UniTask CreateSpaceshipUpperCamera();
        UniTask CreateCollisionPortalCamera();
        UniTask CreateShieldCamera();
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
        UniTask CreatePortalSound();
        UniTask CreateDestroySound();
        UniTask CreateCollisionFx(Vector3 position, Transform parent);
    }
}