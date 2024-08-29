using Assets.RaceTheSun.Sources.MainMenu.Model;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu
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
