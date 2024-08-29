using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu
{
    public class ModelPointCamera : FreeLookCamera
    {
        [Inject]
        private void Construct(ModelPoint modelPoint)
        {
            CinemachineFreeLook.Follow = modelPoint.transform;
            CinemachineFreeLook.LookAt = modelPoint.transform;
        }
    }
}
