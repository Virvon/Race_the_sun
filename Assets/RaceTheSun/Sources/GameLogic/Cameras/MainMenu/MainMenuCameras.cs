using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu
{
    public class MainMenuCameras
    {
        private FreeLookCamera _currentCamera;
        private MainMenuMainCamera _mainMenuMainCamera;
        private SelectionCamera _selectionCamera;
        private CustomizeCamera _customizeCamera;
        private TrailCamera _trailCamera;

        public void Init(MainMenuMainCamera mainMenuMainCamera) =>
            _mainMenuMainCamera = mainMenuMainCamera;

        public void Init(SelectionCamera trailCamera) =>
            _selectionCamera = trailCamera;

        public void Init(CustomizeCamera customizeCamera) =>
            _customizeCamera = customizeCamera;

        public void Init(TrailCamera trailCamera) =>
            _trailCamera = trailCamera;

        public void IncludeCamera(MainMenuCameraType type)
        {
            FreeLookCamera targetCamera = null;

            switch (type)
            {
                case MainMenuCameraType.MainCamera:
                    targetCamera = _mainMenuMainCamera;
                    break;
                case MainMenuCameraType.SelectionCamera:
                    targetCamera = _selectionCamera;
                    break;
                case MainMenuCameraType.CustomizeCamera:
                    targetCamera = _customizeCamera;
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
