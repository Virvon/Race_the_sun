using Cinemachine;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class SpaceshipMainCamera : VirtualCamera
    {
        [SerializeField] public Vector3 _fromFirsPersonFollowOffset;
        [SerializeField] public Vector3 _fromThirdPersonFollowOffset;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(Spaceship.Spaceship spaceship, IPersistentProgressService persistentProgressService)
        {
            CinemachineVirtualCamera.Follow = spaceship.transform;
            _persistentProgressService = persistentProgressService;

            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed += ChangeFollowOffset;

            ChangeFollowOffset();
        }

        private void OnDestroy()
        {
            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed -= ChangeFollowOffset;
        }

        private void ChangeFollowOffset()
        {
            CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = _persistentProgressService.Progress.SpaceshipMainCameraSettings.IsFromThirdPerson ? _fromThirdPersonFollowOffset : _fromFirsPersonFollowOffset;
        }
    }
}
