using Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.MainMenu;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu
{
    public class MainMenuCameras
    {
        private IMainMenuCamerasFactory _mainMenuCamerasFactory;
        private FreeLookCamera _currentCamera;

        [Inject]
        private void Construct(IMainMenuCamerasFactory mainMenuCamerasFactory) =>
            _mainMenuCamerasFactory = mainMenuCamerasFactory;

        public void IncludeCamera(MainMenuCameraType type)
        {
            FreeLookCamera targetCamera = null;

            switch (type)
            {
                case MainMenuCameraType.MainCamera:
                    targetCamera = _mainMenuCamerasFactory.MainMenuInfoCamera;
                    break;
                case MainMenuCameraType.SelectionCamera:
                    targetCamera = _mainMenuCamerasFactory.SelectionCamera;
                    break;
                case MainMenuCameraType.CustomizeCamera:
                    targetCamera = _mainMenuCamerasFactory.CustomizeCamera;
                    break;
                case MainMenuCameraType.TrailCamera:
                    targetCamera = _mainMenuCamerasFactory.TrailCamera;
                    break;
            }

            if (targetCamera == _currentCamera || targetCamera == null)
                return;

            if (_currentCamera != null)
                _currentCamera.CinemachineFreeLook.Priority = (int)CameraPriority.NotUse;

            _currentCamera = targetCamera;
            _currentCamera.ResetPosition();
            _currentCamera.CinemachineFreeLook.Priority = (int)CameraPriority.Use;
        }
    }
}
