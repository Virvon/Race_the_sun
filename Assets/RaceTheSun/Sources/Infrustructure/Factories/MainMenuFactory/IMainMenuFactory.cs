using Assets.RaceTheSun.Sources.Data;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public interface IMainMenuFactory
    {
        UniTask CreateMainMenu();
        UniTask CreateMainMenuMainCamera();
        UniTask CreateModelPoint(Vector3 position);
        UniTask<SpaceshipModel> CreateSpaceshipModel(SpaceshipType type, Vector3 position);
        UniTask CreateTrailCamera();
    }
}