using Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.Gameplay;
using Cinemachine;
using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay
{
    public class GameplayCameras
    {
        private readonly CinemachineBrain _cinemachineBrain;
        private readonly IGameplayCamerasFactory _gameplayCamerasFactory;

        private VirtualCamera _currentCamera;

        public GameplayCameras(IGameplayCamerasFactory gameplayCamerasFactory)
        {
            _gameplayCamerasFactory = gameplayCamerasFactory;

            _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        }

        public SpaceshipMainCamera SpaceshipMainCamera => _gameplayCamerasFactory.SpaceshipMainCamera;

        public void IncludeCamera(GameplayCameraType type)
        {
            VirtualCamera targetCamera = null;

            switch (type)
            {
                case GameplayCameraType.MainCamera:
                    targetCamera = _gameplayCamerasFactory.SpaceshipMainCamera;
                    break;
                case GameplayCameraType.SideCamera:
                    targetCamera = _gameplayCamerasFactory.SpaceshipSideCamera;
                    break;
                case GameplayCameraType.UpperCamera:
                    targetCamera = _gameplayCamerasFactory.SpaceshipUpperCamera;
                    break;
                case GameplayCameraType.StartCamera:
                    targetCamera = _gameplayCamerasFactory.StartCamera;
                    break;
                case GameplayCameraType.CollisionPortalCamera:
                    targetCamera = _gameplayCamerasFactory.CollisionPortalCamera;
                    break;
                case GameplayCameraType.ShieldPortalCamera:
                    targetCamera = _gameplayCamerasFactory.ShieldPortalCamera;
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
    }
}
