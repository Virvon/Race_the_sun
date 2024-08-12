using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class SpaceshipUpperCamera : VirtualCamera
    {
        [Inject]
        private void Construct(Spaceship.Spaceship spaceship)
        {
            CinemachineVirtualCamera.Follow = spaceship.transform;
            CinemachineVirtualCamera.LookAt = spaceship.transform;
        }
    }
}
