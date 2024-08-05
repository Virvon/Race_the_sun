using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class TrailWindow : OpenableWindow
    {
        private MainMenuCameras _mainMenuCameras;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras)
        {
            _mainMenuCameras = mainMenuCameras;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.TrailCamera);
            gameObject.SetActive(true);
        }
    }
}