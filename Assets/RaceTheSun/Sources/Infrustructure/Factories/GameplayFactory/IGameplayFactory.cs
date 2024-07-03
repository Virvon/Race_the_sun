using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory
{
    public interface IGameplayFactory
    {
        UniTask CreateHud();
        UniTask CreateSpaceship();
        UniTask<GameObject> CreateTile(Vector3 position, Transform parent);
        UniTask CreateWorldGenerator();
    }
}