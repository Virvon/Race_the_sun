using Assets.RaceTheSun.Sources.Data;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrustructure.Factories.MainMenuFactory
{
    public interface IMainMenuFactory
    {
        UniTask CreateMainMenu();
        UniTask CreateMainMenuMainCamera();
        UniTask CreateModelPoint(Vector3 position);
        UniTask CreateSelectionCamera();
        UniTask CreateCustomizeCamera();
        UniTask CreateTrailCamera();
        UniTask CreateTrailPoint(Vector3 position);
    }
}