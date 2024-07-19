using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cinemachine;
using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class Cameras
    {
        private readonly CinemachineBrain _cinemachineBrain;

        private VirtualCamera _currentCamera;
        private SpaceshipMainCamera _spaceshipMainCamera;
        private SpaceshipSideCamera _spaceshipSideCamera;
        private SpaceshipUpperCamera _spaceshipUpperCamera;
        private StartCamera _startCamera;
        private ShieldPortalCamera _shieldPortalCamera;
        private CollisionPortalCamera _collisionPortalCamera;
        private SpaceshipShieldPortal _spaceshipShieldPortal;

        public Cameras()
        {
            _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        }

        public void Init(SpaceshipMainCamera spaceshipMainCamera)
        {
            _spaceshipMainCamera = spaceshipMainCamera;
        }

        public void Init(SpaceshipSideCamera spaceshipSideCamera)
        {
            _spaceshipSideCamera = spaceshipSideCamera;
        }

        public void Init(SpaceshipUpperCamera spaceshipUpperCamera)
        {
            _spaceshipUpperCamera = spaceshipUpperCamera;
        }

        public void Init(StartCamera startCamera)
        {
            _startCamera = startCamera;
        }

        public void Init(ShieldPortalCamera shieldPortalCamera)
        {
            _shieldPortalCamera = shieldPortalCamera;
        }

        public void Init(CollisionPortalCamera collisionPortalCamera)
        {
            _collisionPortalCamera = collisionPortalCamera;
        }

        public void IncludeCamera(CameraType type)
        {
            VirtualCamera targetCamera = null;

            switch (type)
            {
                case CameraType.MainCamera:
                    targetCamera = _spaceshipMainCamera;
                    break;
                case CameraType.SideCamera:
                    targetCamera = _spaceshipSideCamera;
                    break;
                case CameraType.UpperCamera:
                    targetCamera = _spaceshipUpperCamera;
                    break;
                case CameraType.StartCamera:
                    targetCamera = _startCamera;
                    break;
                case CameraType.CollisionPortalCamera:
                    targetCamera = _collisionPortalCamera;
                    break;
                case CameraType.ShieldPortalCamera:
                    targetCamera = _shieldPortalCamera;
                    break;
            }

            if (targetCamera == _currentCamera || targetCamera == null)
                return;

            if(_currentCamera != null)
                _currentCamera.CinemachineVirtualCamera.Priority = (int)CameraPriority.NotUse;

            _currentCamera = targetCamera;
            _cinemachineBrain.m_DefaultBlend = _currentCamera.BlendDefinition;
            _currentCamera.CinemachineVirtualCamera.Priority = (int)CameraPriority.Use;
        }

        
    }
}
