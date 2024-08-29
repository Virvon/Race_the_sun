using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.MainMenu
{
    public class MainMenuCamerasFactory : IMainMenuCamerasFactory
    {
        private readonly FreeLookCamera.Factory _freeLookCameraFactory;

        public MainMenuCamerasFactory(FreeLookCamera.Factory freeLookCameraFactory) =>
            _freeLookCameraFactory = freeLookCameraFactory;

        public FreeLookCamera SelectionCamera { get; private set; }
        public FreeLookCamera MainMenuInfoCamera { get; private set; }
        public FreeLookCamera CustomizeCamera { get; private set; }
        public FreeLookCamera TrailCamera { get; private set; }

        public async UniTask CreateSelectionCamera() =>
            SelectionCamera = await _freeLookCameraFactory.Create(MainMenuCamerasFactoryAssets.SelectionCamera);

        public async UniTask CreateMainMenuMainCamera() =>
            MainMenuInfoCamera = await _freeLookCameraFactory.Create(MainMenuCamerasFactoryAssets.MainMenuInfoCamera);

        public async UniTask CreateCustomizeCamera() =>
            CustomizeCamera = await _freeLookCameraFactory.Create(MainMenuCamerasFactoryAssets.CustomizeCamera);

        public async UniTask CreateTrailCamera() =>
            TrailCamera = await _freeLookCameraFactory.Create(MainMenuCamerasFactoryAssets.TrailCamera);
    }
}
