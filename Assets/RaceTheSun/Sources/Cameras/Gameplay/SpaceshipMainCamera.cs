using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class SpaceshipMainCamera : VirtualCamera
    {
        [SerializeField] private Vector3 _fromFirsPersonFollowOffset;
        [SerializeField] private Vector3 _fromThirdPersonFollowOffset;
        [SerializeField] private CameraShake _cameraShake;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(Spaceship.Spaceship spaceship, IPersistentProgressService persistentProgressService)
        {
            CinemachineVirtualCamera.Follow = spaceship.transform;
            _persistentProgressService = persistentProgressService;

            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed += ChangeFollowOffset;

            ChangeFollowOffset();
        }

        private void OnDestroy() =>
            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed -= ChangeFollowOffset;

        public void Shake()
        {
            _cameraShake.Shake();
        }

        private void ChangeFollowOffset() =>
            CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = _persistentProgressService.Progress.SpaceshipMainCameraSettings.IsFromThirdPerson ? _fromThirdPersonFollowOffset : _fromFirsPersonFollowOffset;
    }
}
