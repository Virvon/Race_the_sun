using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class SpaceshipSideCamera : VirtualCamera
    {
        [Inject]
        private void Construct(Spaceship.Spaceship spaceship) =>
            CinemachineVirtualCamera.Follow = spaceship.transform;
    }
}
