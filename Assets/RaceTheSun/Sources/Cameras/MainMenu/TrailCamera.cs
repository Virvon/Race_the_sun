using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class TrailCamera : FreeLookCamera
    {
        [Inject]
        private void Construct(TrailPoint trailPoint)
        {
            CinemachineFreeLook.LookAt = trailPoint.transform;
            CinemachineFreeLook.Follow = trailPoint.transform;
        }
    }
}
