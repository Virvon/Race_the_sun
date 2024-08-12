using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class ModelPointCamera : FreeLookCamera
    {
        [Inject]
        private void Construct(Assets.RaceTheSun.Sources.MainMenu.ModelPoint.ModelPoint modelPoint)
        {
            CinemachineFreeLook.Follow = modelPoint.transform;
            CinemachineFreeLook.LookAt = modelPoint.transform;
        }
    }
}
