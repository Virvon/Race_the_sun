using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class MainMenuMainCamera : FreeLookCamera
    {
        [Inject]
        private void Construct(ModelPoint modelPoint)
        {
            CinemachineFreeLook.Follow = modelPoint.transform;
            CinemachineFreeLook.LookAt = modelPoint.transform;
        }
    }
}
