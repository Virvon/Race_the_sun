using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay
{
    public class SpaceshipMainCamera : VirtualCamera
    {
        private const float DefaultSpeed = 300;

        [SerializeField] private Vector3 _fromFirsPersonFollowOffset;
        [SerializeField] private Vector3 _fromThirdPersonFollowOffset;
        [SerializeField] private CameraShake _cameraShake;
        [SerializeField] private float _fovMultiplier;

        private IPersistentProgressService _persistentProgressService;
        private float _startFov;
        private Spaceship _spaceship;

        [Inject]
        private void Construct(Spaceship spaceship, IPersistentProgressService persistentProgressService)
        {
            _spaceship = spaceship;
            CinemachineVirtualCamera.Follow = spaceship.transform;
            _persistentProgressService = persistentProgressService;
            _startFov = CinemachineVirtualCamera.m_Lens.FieldOfView;
            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed += ChangeFollowOffset;

            ChangeFollowOffset();
        }

        private void OnDestroy() =>
            _persistentProgressService.Progress.SpaceshipMainCameraSettings.Changed -= ChangeFollowOffset;

        private void Update()
        {
            float targetFov = _spaceship.Speed / DefaultSpeed * _fovMultiplier * _startFov;

            targetFov = targetFov <= _startFov ? _startFov : targetFov;

            CinemachineVirtualCamera.m_Lens.FieldOfView = targetFov;
        }

        public void Shake()
        {
            _cameraShake.Shake();
        }

        private void ChangeFollowOffset() =>
            CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = _persistentProgressService.Progress.SpaceshipMainCameraSettings.IsFromThirdPerson ? _fromThirdPersonFollowOffset : _fromFirsPersonFollowOffset;
    }
}
