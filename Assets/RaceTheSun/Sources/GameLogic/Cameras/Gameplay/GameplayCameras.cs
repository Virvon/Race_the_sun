using Cinemachine;
using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay
{
    public class GameplayCameras
    {
        private readonly CinemachineBrain _cinemachineBrain;

        private VirtualCamera _currentCamera;
        private SpaceshipMainCamera _spaceshipMainCamera;
        private SpaceshipSideCamera _spaceshipSideCamera;
        private SpaceshipUpperCamera _spaceshipUpperCamera;
        private StartCamera _startCamera;
        private ShieldPortalCamera _shieldPortalCamera;
        private CollisionPortalCamera _collisionPortalCamera;

        public GameplayCameras() =>
            _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        public void Init(SpaceshipMainCamera spaceshipMainCamera) =>
            _spaceshipMainCamera = spaceshipMainCamera;

        public void Init(SpaceshipSideCamera spaceshipSideCamera) =>
            _spaceshipSideCamera = spaceshipSideCamera;

        public void Init(SpaceshipUpperCamera spaceshipUpperCamera) =>
            _spaceshipUpperCamera = spaceshipUpperCamera;

        public void Init(StartCamera startCamera) =>
            _startCamera = startCamera;

        public void Init(ShieldPortalCamera shieldPortalCamera) =>
            _shieldPortalCamera = shieldPortalCamera;

        public void Init(CollisionPortalCamera collisionPortalCamera) =>
            _collisionPortalCamera = collisionPortalCamera;

        public void IncludeCamera(GameplayCameraType type)
        {
            VirtualCamera targetCamera = null;

            switch (type)
            {
                case GameplayCameraType.MainCamera:
                    targetCamera = _spaceshipMainCamera;
                    break;
                case GameplayCameraType.SideCamera:
                    targetCamera = _spaceshipSideCamera;
                    break;
                case GameplayCameraType.UpperCamera:
                    targetCamera = _spaceshipUpperCamera;
                    break;
                case GameplayCameraType.StartCamera:
                    targetCamera = _startCamera;
                    break;
                case GameplayCameraType.CollisionPortalCamera:
                    targetCamera = _collisionPortalCamera;
                    break;
                case GameplayCameraType.ShieldPortalCamera:
                    targetCamera = _shieldPortalCamera;
                    break;
            }

            if (targetCamera == _currentCamera || targetCamera == null)
                return;

            if (_currentCamera != null)
                _currentCamera.CinemachineVirtualCamera.Priority = (int)CameraPriority.NotUse;

            _currentCamera = targetCamera;
            _cinemachineBrain.m_DefaultBlend = _currentCamera.BlendDefinition;
            _currentCamera.CinemachineVirtualCamera.Priority = (int)CameraPriority.Use;
        }

        public void ShakeSpaceshipMainCamera()
        {
            _spaceshipMainCamera.Shake();
        }
    }
}
