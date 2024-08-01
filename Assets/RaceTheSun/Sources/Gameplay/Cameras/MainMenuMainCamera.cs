using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class MainMenuMainCamera : FreeLookCamera
    {
        [Inject]
        private void Construct(SpaceshipModel spaceshipModel)
        {
            CinemachineFreeLook.Follow = spaceshipModel.transform;
            CinemachineFreeLook.LookAt = spaceshipModel.transform;
        }
    }
}
