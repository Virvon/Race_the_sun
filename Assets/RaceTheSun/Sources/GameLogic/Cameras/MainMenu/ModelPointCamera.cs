using Assets.RaceTheSun.Sources.MainMenu.Model;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu
{
    public class ModelPointCamera : FreeLookCamera
    {
        [Inject]
        private void Construct(ModelSpawner modelPoint)
        {
            CinemachineFreeLook.Follow = modelPoint.transform;
            CinemachineFreeLook.LookAt = modelPoint.transform;
        }
    }
}
