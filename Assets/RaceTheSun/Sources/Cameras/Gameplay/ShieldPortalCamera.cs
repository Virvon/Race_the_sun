using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class ShieldPortalCamera : VirtualCamera
    {
        [Inject]
        private void Construct(Spaceship.Spaceship spaceship, SpaceshipShieldPortal spaceshipShieldPortal)
        {
            CinemachineVirtualCamera.Follow = spaceshipShieldPortal.transform;
            CinemachineVirtualCamera.LookAt = spaceshipShieldPortal.transform;
        }
    }
}
