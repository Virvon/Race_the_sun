using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cinemachine;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay
{
    public class ShieldPortalCamera : VirtualCamera
    {
        [Inject]
        private void Construct(Spaceship spaceship, SpaceshipShieldPortal spaceshipShieldPortal)
        {
            CinemachineVirtualCamera.Follow = spaceshipShieldPortal.transform;
            CinemachineVirtualCamera.LookAt = spaceshipShieldPortal.transform;
        }
    }
}
