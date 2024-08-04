using System;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class MainMenuCameras
    {
        private FreeLookCamera _currentCamera;
        private MainMenuMainCamera _mainMenuMainCamera;
        private SelectionCamera _trailCamera;

        public void Init(MainMenuMainCamera mainMenuMainCamera)
        {
            _mainMenuMainCamera = mainMenuMainCamera;
        }

        public void Init(SelectionCamera trailCamera)
        {
            _trailCamera = trailCamera;
        }

        public void IncludeCamera(MainMenuCameraType type)
        {
            FreeLookCamera targetCamera = null;

            switch (type)
            {
                case MainMenuCameraType.MainCamera:
                    targetCamera = _mainMenuMainCamera;
                    break;
                case MainMenuCameraType.TrailCamera:
                    targetCamera = _trailCamera;
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
