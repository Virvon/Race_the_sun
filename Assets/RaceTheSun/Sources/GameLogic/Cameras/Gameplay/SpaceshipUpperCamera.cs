using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay
{
    public class SpaceshipUpperCamera : VirtualCamera
    {
        [Inject]
        private void Construct(Spaceship spaceship)
        {
            CinemachineVirtualCamera.Follow = spaceship.transform;
            CinemachineVirtualCamera.LookAt = spaceship.transform;
        }
    }
}
