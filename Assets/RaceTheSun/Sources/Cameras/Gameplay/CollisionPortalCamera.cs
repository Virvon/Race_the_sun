using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class CollisionPortalCamera : VirtualCamera
    {
        [Inject]
        private void Construct(Spaceship.Spaceship spaceship)
        {
            Transform target = spaceship.GetComponentInChildren<CollisionPortalPoint>().transform;

            CinemachineVirtualCamera.Follow = target;
            CinemachineVirtualCamera.LookAt = target;
        }
    }
}
