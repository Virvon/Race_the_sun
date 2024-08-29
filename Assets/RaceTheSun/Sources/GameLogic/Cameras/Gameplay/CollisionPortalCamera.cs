using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay
{
    public class CollisionPortalCamera : VirtualCamera
    {
        [Inject]
        private void Construct(Spaceship spaceship)
        {
            Transform target = spaceship.GetComponentInChildren<CollisionPortalPoint>().transform;

            CinemachineVirtualCamera.Follow = target;
            CinemachineVirtualCamera.LookAt = target;
        }
    }
}
