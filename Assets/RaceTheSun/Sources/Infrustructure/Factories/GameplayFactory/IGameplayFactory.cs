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
        UniTask CreateSpaceship();
        UniTask<GameObject> CreateTile(AssetReferenceGameObject tileReference, Vector3 position, Transform parent);
        UniTask CreateWorldGenerator();
        UniTask CreateStartCamera();
        UniTask CreateSpaceshipMainCamera();
        UniTask CreateSpaceshipSideCamera();
        UniTask CreateSun();
    }
}