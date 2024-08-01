using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class MainInfoWindow : OpenableWindow
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
            gameObject.SetActive(true);
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.MainCamera);
        }
    }
}