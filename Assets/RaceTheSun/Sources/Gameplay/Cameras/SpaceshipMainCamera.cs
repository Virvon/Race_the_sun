using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class SpaceshipMainCamera : VirtualCamera
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        [Inject]
        private void Construct(Spaceship.Spaceship spaceship)
        {
            _cinemachineVirtualCamera.Follow = spaceship.transform;
        }
    }
}
