using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.MainMenu
{
    public interface IMainMenuCamerasFactory
    {
        FreeLookCamera CustomizeCamera { get; }
        FreeLookCamera MainMenuInfoCamera { get; }
        FreeLookCamera SelectionCamera { get; }
        FreeLookCamera TrailCamera { get; }

        UniTask CreateCustomizeCamera();
        UniTask CreateMainMenuMainCamera();
        UniTask CreateSelectionCamera();
        UniTask CreateTrailCamera();
    }
}